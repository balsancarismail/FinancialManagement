namespace Application.Features.Budget.Commands.Create;

public class CreateBudgetResponse
{
    public int Id { get; set; }
    public int AppUserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}