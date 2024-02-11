using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Investment : Entity<int>
{
    public int PortfolioId { get; set; }
    public string Type { get; set; } // e.g., Stock, Bond
    public decimal Amount { get; set; }
    public DateTime PurchaseDate { get; set; }

    // Navigation property
    public InvestmentPortfolio Portfolio { get; set; }
}