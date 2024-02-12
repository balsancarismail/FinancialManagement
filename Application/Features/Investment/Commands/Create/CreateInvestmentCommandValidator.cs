using FluentValidation;

namespace Application.Features.Investment.Commands.Create;

public class CreateInvestmentCommandValidator : AbstractValidator<CreateInvestmentCommand>
{
    public CreateInvestmentCommandValidator()
    {
        RuleFor(c => c.Amount).NotNull().NotEmpty().GreaterThan(0);
        RuleFor(c => c.PurchaseDate).NotNull().NotEmpty();
        RuleFor(c => c.PortfolioId).NotNull().NotEmpty().GreaterThan(0);
    }
}