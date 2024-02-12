using FluentValidation;

namespace Application.Features.InvestmentPortfolio.Commands.Update;

public class UpdateInvestmentPortfolioCommandValidator : AbstractValidator<UpdateInvestmentPortfolioCommand>
{
    public UpdateInvestmentPortfolioCommandValidator()
    {
        RuleFor(i => i.Id).NotNull().GreaterThan(0);
        RuleFor(i => i.Name).NotNull().NotEmpty();
        RuleFor(i => i.Description).NotNull().NotEmpty();
    }
}