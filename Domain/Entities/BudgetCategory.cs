using Core.Persistence.Repositories;

namespace Domain.Entities;

public class BudgetCategory : Entity<int>
{
    public int BudgetId { get; set; }
    public int CategoryId { get; set; }
    public decimal AllocatedAmount { get; set; }

    // Navigation properties
    public virtual Budget Budget { get; set; }
    public virtual Category Category { get; set; }
}