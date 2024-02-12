using FluentValidation;

namespace Application.Features.InvestmentPortfolio.Commands.Create;

public class CreateInvestmentPortfolioCommandValidator : AbstractValidator<CreateInvestmentPortfolioCommand>
{
    public CreateInvestmentPortfolioCommandValidator()
    {
        RuleFor(i => i.Name).NotNull().NotEmpty();
        RuleFor(i => i.Description).NotNull().NotEmpty();
    }
}