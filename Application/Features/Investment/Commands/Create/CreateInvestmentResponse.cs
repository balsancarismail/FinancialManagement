using Domain.Enums;

namespace Application.Features.Investment.Commands.Create;

public class CreateInvestmentResponse
{
    public int PortfolioId { get; set; }
    public InvestmentType InvestmentType { get; set; } // e.g., Stock, Bond
    public decimal Amount { get; set; }
    public DateTime PurchaseDate { get; set; }
}