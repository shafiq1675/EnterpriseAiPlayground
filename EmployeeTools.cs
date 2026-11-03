using EnterpriseAiPlayground.Models;
using EnterpriseAiPlayground.Services;
using System.ComponentModel;

namespace EnterpriseAiPlayground;

public class EmployeeTools
{
    private readonly EmployeeLeaveService _leaveService;

    public EmployeeTools(EmployeeLeaveService leaveService)
    {
        _leaveService = leaveService;
    }

    [Description(
        "Retrieves annual leave information for an employee. " +
        "Use this when the user asks about annual leave, used leave, " +
        "remaining leave, or leave balance.")]
    public async Task<EmployeeLeaveResult> GetEmployeeLeave(
        [Description("The employee ID.")]
        int employeeId)
    {
        Console.WriteLine(
            $"TOOL EXECUTED: EmployeeId = {employeeId}");

        return await _leaveService.GetEmployeeLeaveAsync(employeeId);
    }
}