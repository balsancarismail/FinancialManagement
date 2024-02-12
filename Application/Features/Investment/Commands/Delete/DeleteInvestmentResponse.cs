using Domain.Enums;

namespace Application.Features.Investment.Commands.Delete;

public class DeleteInvestmentResponse
{
    public int PortfolioId { get; set; }
    public InvestmentType InvestmentType { get; set; } // e.g., Stock, Bond
    public decimal Amount { get; set; }
    public DateTime PurchaseDate { get; set; }
}