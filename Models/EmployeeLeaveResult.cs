namespace EnterpriseAiPlayground.Models;

public class EmployeeLeaveResult
{
    public bool Found { get; set; }

    public int EmployeeId { get; set; }

    public decimal AnnualAllowanceDays { get; set; }

    public decimal UsedDays { get; set; }

    public decimal RemainingDays { get; set; }

    public string? Message { get; set; }
}

public class ConversationContext
{
    public int? CurrentEmployeeId { get; set; }
}