namespace Application.Features.Budget.Queries.GetById;

public class GetByIdBudgetResponse
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

}