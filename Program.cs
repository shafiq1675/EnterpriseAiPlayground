using EnterpriseAiPlayground;
using EnterpriseAiPlayground.Data;
using EnterpriseAiPlayground.Models;
using EnterpriseAiPlayground.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using OllamaSharp;

var services = new ServiceCollection();

services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql("Host=localhost;Port=5432;Database=enterprise_ai_db;Username=postgres;Password=sa@123"));

services.AddScoped<EmployeeLeaveService>();

var serviceProvider = services.BuildServiceProvider();

var leaveService =    serviceProvider.GetRequiredService<EmployeeLeaveService>();

var conversationStore = new ConversationStore();

IChatClient chatClient = new OllamaApiClient(new Uri("http://localhost:11434"), "llama3.1:8b");

chatClient = chatClient
    .AsBuilder()
    .UseFunctionInvocation()
    .Build();

var employeeTools = new EmployeeTools(leaveService);

var tools = new[]
{
    AIFunctionFactory.Create(        employeeTools.GetEmployeeLeave)
};

var options = new ChatOptions
{
    Tools = [.. tools],
    ToolMode = ChatToolMode.RequireAny
};

Console.Write("Conversation ID: ");
var conversationId = Console.ReadLine();

if (string.IsNullOrWhiteSpace(conversationId))
    return;

var messages = conversationStore.GetOrCreate(conversationId);


while (true)
{
    
    Console.Write("\nYou: ");
    var prompt = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(prompt))
        continue;

    if (prompt.Equals("exit", StringComparison.OrdinalIgnoreCase))
        break;

    messages.Add(new ChatMessage(ChatRole.User, prompt));

    var response = await chatClient.GetResponseAsync(messages, options);

    Console.WriteLine("\nAI: " + response.Text);

    messages.AddRange(response.Messages);
}