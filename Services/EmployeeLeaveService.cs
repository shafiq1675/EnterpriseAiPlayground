using EnterpriseAiPlayground.Data;
using EnterpriseAiPlayground.Models;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseAiPlayground.Services;

public class EmployeeLeaveService
{
    private readonly ApplicationDbContext _context;

    public EmployeeLeaveService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<EmployeeLeaveResult> GetEmployeeLeaveAsync(
    int employeeId)
    {
        var employee = await _context.EmployeeLeaveBalances
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.EmployeeId == employeeId);

        if (employee == null)
        {
            return new EmployeeLeaveResult
            {
                Found = false,
                EmployeeId = employeeId,
                Message = $"Employee {employeeId} was not found."
            };
        }

        return new EmployeeLeaveResult
        {
            Found = true,
            EmployeeId = employee.EmployeeId,
            AnnualAllowanceDays = employee.AnnualAllowanceDays,
            UsedDays = employee.UsedDays,
            RemainingDays = employee.RemainingDays
        };
    }
}