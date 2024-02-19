namespace Application.Features.Budget.Queries.GetBudgetBreakdown;

public class GetBudgetBreakdownResponse
{
    public IList<BudgetCategoryBreakdownDto> BudgetCategoryBreakdownDtos { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public decimal TotalAimedAllocatedAmount { get; set; }
    public decimal TotalAllocatedAmount { get; set; }
    public decimal TotalAimedSpentAmount { get; set; }
    public decimal TotalSpentAmount { get; set; }
    public decimal TotalProfitOrLoss => TotalAllocatedAmount - TotalSpentAmount;
    public decimal TotalAimedProfitOrLoss => TotalAimedAllocatedAmount - TotalAimedSpentAmount;
    public decimal TotalPercentageProfitOrLoss => TotalProfitOrLoss / TotalAllocatedAmount * 100;
    public decimal TotalAimedPercentageProfitOrLoss => TotalAimedProfitOrLoss / TotalAimedAllocatedAmount * 100;
    public bool IsOverBudget => TotalSpentAmount > TotalAllocatedAmount;
}