namespace Application.Features.BudgetCategory.Commands.Update;

public class UpdateBudgetCategoryResponse
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public int BudgetId { get; set; }
    public decimal AllocatedAmount { get; set; }
}