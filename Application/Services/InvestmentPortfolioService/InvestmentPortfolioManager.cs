using Application.Services.Repositories;
using Domain.Entities;

namespace Application.Services.InvestmentPortfolioService;

public class InvestmentPortfolioManager(IInvestmentPortfolioRepository investmentPortfolioRepository)
    : IInvestmentPortfolioService
{
    private readonly IInvestmentPortfolioRepository _investmentPortfolioRepository = investmentPortfolioRepository;

    public async Task<InvestmentPortfolio> GetInvestmentPortfolioByIdAsync(int id, CancellationToken cancellationToken)
    {
        var investmentPortfolio = await _investmentPortfolioRepository.GetAsync(
            ip => ip.Id == id, cancellationToken: cancellationToken
        );
        return investmentPortfolio;
    }
}