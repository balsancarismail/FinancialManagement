namespace Application.Features.Budget.Commands.Delete;

public class DeleteBudgetResponse
{
    public int Id { get; set; }
    public int AppUserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}