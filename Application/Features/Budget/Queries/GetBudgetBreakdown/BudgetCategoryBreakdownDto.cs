using Domain.Enums;

namespace Application.Features.Budget.Queries.GetBudgetBreakdown;

public class BudgetCategoryBreakdownDto
{
    public int Id { get; set; }
    public string CategoryName { get; set; } = null!;
    public CategoryType CategoryType { get; set; }
    public decimal AllocatedAmount { get; set; }
    public decimal OperationsTotal { get; set; }
    public decimal ProfitOrLoss => (AllocatedAmount - OperationsTotal) * (CategoryType == CategoryType.Income ? -1 : 0);
    public decimal PercentageProfitOrLoss => ProfitOrLoss / AllocatedAmount * 100;
    public bool IsOverBudget => OperationsTotal > AllocatedAmount;
}