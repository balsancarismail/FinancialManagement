using FluentValidation;

namespace Application.Features.InvestmentPortfolio.Commands.Delete;

public class DeleteInvestmentPortfolioCommandValidator : AbstractValidator<DeleteInvestmentPortfolioCommand>
{
    public DeleteInvestmentPortfolioCommandValidator()
    {
        RuleFor(i => i.Id).NotNull().GreaterThan(0);
    }
}