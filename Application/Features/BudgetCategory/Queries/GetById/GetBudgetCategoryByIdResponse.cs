namespace Application.Features.BudgetCategory.Queries.GetById;

public class GetBudgetCategoryByIdResponse
{
    public int Id { get; set; }
    public string CategoryName { get; set; }
    public decimal AllocatedAmount { get; set; }
}