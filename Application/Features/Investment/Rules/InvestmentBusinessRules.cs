using Application.Features.Investment.Constants;
using Core.CrossCuttingConcerns.Exceptions.Types;

namespace Application.Features.Investment.Rules;

public class InvestmentBusinessRules : IInvestmentBusinessRules
{
    public void InvestmentPortfolioMustBeExists(Domain.Entities.InvestmentPortfolio investmentPortfolio)
    {
        if (investmentPortfolio == null)
            throw new BusinessException(InvestmentMessages.InvestmentPortfolioMustBeExists);
    }

    public void InvestmentMustBeExists(Domain.Entities.Investment investment)
    {
        if (investment == null) throw new BusinessException(InvestmentMessages.InvestmentMustBeExists);
    }
}
