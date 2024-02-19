using Domain.Enums;

namespace Application.Features.InvestmentPortfolio.Queries.GetInvestmentPortfolioBreakdown;

public class InvestmentBreakdownDto
{
    public int PortfolioId { get; set; }
    public InvestmentType InvestmentType { get; set; } // e.g., Stock, Bond
    public decimal Amount { get; set; }
    public decimal RandomProfitOrLoss { get; set; }
    public decimal ProfitOrLossPercentage => (RandomProfitOrLoss - Amount) / Amount * 100;
    public DateTime PurchaseDate { get; set; }
}