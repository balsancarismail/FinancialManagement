using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Domain.Entities;

public class FinancialTransaction : Entity<int>
{
    public int AppUserId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public int CategoryId { get; set; }
    public string Description { get; set; }

    // Navigation properties
    public virtual Category Category { get; set; }
    public virtual AppUser AppUser { get; set; }
}