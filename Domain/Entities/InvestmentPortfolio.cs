using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Domain.Entities;

public class InvestmentPortfolio : Entity<int>
{
    public int AppUserId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    // Navigation properties
    public virtual ICollection<Investment> Investments { get; set; }
    public virtual AppUser AppUser { get; set; }
}