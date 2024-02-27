namespace Application.Features.InvestmentPortfolio.Rules;

public interface IInvestmentPortfolioBusinessRules
{
    Task InvestmentPortfolioMustNotBeNull(Domain.Entities.InvestmentPortfolio investmentPortfolio);
}