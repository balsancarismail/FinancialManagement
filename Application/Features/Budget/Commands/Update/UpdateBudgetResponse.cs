namespace Application.Features.Budget.Commands.Update;

public class UpdateBudgetResponse
{
    public int AppUserId { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}