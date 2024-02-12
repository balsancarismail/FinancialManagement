namespace Application.Features.BudgetCategory.Commands.Delete;

public class DeleteBudgetCategoryResponse
{
    public int CategoryId { get; set; }
    public int BudgetId { get; set; }
    public decimal AllocatedAmount { get; set; }
}