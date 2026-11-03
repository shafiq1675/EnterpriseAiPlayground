namespace EnterpriseAiPlayground.Entities;

public class EmployeeLeaveBalance
{
    public long Id { get; set; }

    public int EmployeeId { get; set; }

    public decimal AnnualAllowanceDays { get; set; }

    public decimal UsedDays { get; set; }

    public DateTime UpdatedAt { get; set; }

    public decimal RemainingDays =>
        AnnualAllowanceDays - UsedDays;
}