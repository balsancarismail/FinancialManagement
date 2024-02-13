namespace Application.Features.BudgetCategory.Commands.Create;

public class CreateBudgetCategoryResponse
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public int BudgetId { get; set; }
    public decimal AllocatedAmount { get; set; }
}