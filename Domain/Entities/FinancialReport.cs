namespace Domain.Entities;

public class FinancialReport
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public DateTime CreatedDate { get; set; }
    public string ReportType { get; set; } // e.g., BudgetReport, InvestmentReport
    public string Content { get; set; }
}