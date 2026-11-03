using Microsoft.Extensions.AI;

namespace EnterpriseAiPlayground.Models;

public class ConversationStore
{
    private readonly Dictionary<string, List<ChatMessage>> _conversations = new();

    public List<ChatMessage> GetOrCreate(string conversationId)
    {
        if (!_conversations.TryGetValue(conversationId, out var messages))
        {
            messages = new List<ChatMessage>
            {
                new(
                    ChatRole.System,
                    """
                    You are an enterprise HR assistant.

                    When the user asks about employee leave:
                    - Call GetEmployeeLeave.
                    - Use only the returned data.
                    - Never invent employee IDs or leave information.
                    - AnnualAllowanceDays means total annual entitlement.
                    - UsedDays means leave already consumed.
                    - RemainingDays means leave still available.
                    """)
            };

            _conversations[conversationId] = messages;
        }

        return messages;
    }
}
