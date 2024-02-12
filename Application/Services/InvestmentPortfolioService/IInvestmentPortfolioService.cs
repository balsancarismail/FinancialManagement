using Domain.Entities;

namespace Application.Services.InvestmentPortfolioService;

public interface IInvestmentPortfolioService
{
    public Task<InvestmentPortfolio> GetInvestmentPortfolioByIdAsync(int id, CancellationToken cancellationToken);
}