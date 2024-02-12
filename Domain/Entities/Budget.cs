using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Domain.Entities;

public class Budget: Entity<int>
{
    public int AppUserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }


    // Navigation properties
    public ICollection<BudgetCategory> BudgetCategories { get; set; }
    public virtual AppUser AppUser { get; set; }
}