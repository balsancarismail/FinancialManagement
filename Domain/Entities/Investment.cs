using Core.Persistence.Repositories;
using Domain.Enums;

namespace Domain.Entities;

public class Investment : Entity<int>
{
    public int PortfolioId { get; set; }
    public InvestmentType InvestmentType { get; set; } // e.g., Stock, Bond
    public decimal Amount { get; set; }
    public decimal RandomProfitOrLoss { get; set; }
    public DateTime PurchaseDate { get; set; }

    // Navigation property
    public InvestmentPortfolio Portfolio { get; set; }
}