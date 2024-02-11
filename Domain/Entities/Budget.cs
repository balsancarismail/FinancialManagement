using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Domain.Entities;

public class Budget: Entity<int>
{
    public string UserId { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }


    // Navigation properties
    public ICollection<BudgetCategory> BudgetCategories { get; set; }
    public virtual AppUser User { get; set; }
}