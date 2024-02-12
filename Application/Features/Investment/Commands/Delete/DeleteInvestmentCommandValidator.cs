using FluentValidation;

namespace Application.Features.Investment.Commands.Delete;

public class DeleteInvestmentCommandValidator : AbstractValidator<DeleteInvestmentCommand>
{
    public DeleteInvestmentCommandValidator()
    {
        RuleFor(c => c.Id).NotNull().NotEmpty().GreaterThan(0);
    }
}