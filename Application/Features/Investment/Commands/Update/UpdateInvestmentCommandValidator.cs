using FluentValidation;

namespace Application.Features.Investment.Commands.Update;

public class UpdateInvestmentCommandValidator : AbstractValidator<UpdateInvestmentCommand>
{
    public UpdateInvestmentCommandValidator()
    {
        RuleFor(c => c.Amount).NotNull().NotEmpty().GreaterThan(0);
        RuleFor(c => c.Id).NotNull().NotEmpty().GreaterThan(0);
        RuleFor(c => c.PurchaseDate).NotNull().NotEmpty();
    }
}