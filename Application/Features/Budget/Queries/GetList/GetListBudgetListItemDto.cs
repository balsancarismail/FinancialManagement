namespace Application.Features.Budget.Queries.GetList;

public class GetListBudgetListItemDto
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; } 
}