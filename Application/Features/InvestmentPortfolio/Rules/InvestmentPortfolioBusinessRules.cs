using Application.Features.InvestmentPortfolio.Constants;
using Core.CrossCuttingConcerns.Exceptions.Types;

namespace Application.Features.InvestmentPortfolio.Rules;

public class InvestmentPortfolioBusinessRules : IInvestmentPortfolioBusinessRules
{
    public Task InvestmentPortfolioMustNotBeNull(Domain.Entities.InvestmentPortfolio investmentPortfolio)
    {
        if (investmentPortfolio is null)
            throw new BusinessException(InvestmentPorfolioMessages.InvestmentPortfolioNotFound);
        return Task.CompletedTask;
    }
}
