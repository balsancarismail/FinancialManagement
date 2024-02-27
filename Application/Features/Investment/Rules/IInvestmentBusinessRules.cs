namespace Application.Features.Investment.Rules;

public interface IInvestmentBusinessRules
{
    void InvestmentPortfolioMustBeExists(Domain.Entities.InvestmentPortfolio investmentPortfolio);
    void InvestmentMustBeExists(Domain.Entities.Investment investment);
}