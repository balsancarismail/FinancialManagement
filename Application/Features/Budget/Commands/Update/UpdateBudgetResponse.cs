namespace Application.Features.Budget.Commands.Update;

public class UpdateBudgetResponse
{
    public int Id { get; set; }
    public int AppUserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}