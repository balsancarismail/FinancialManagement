using FluentValidation;

namespace Application.Features.Budget.Commands.Delete;

public class DeleteBudgetCommandValidator : AbstractValidator<DeleteBudgetCommand>
{
    public DeleteBudgetCommandValidator()
    {
        RuleFor(c => c.Id).NotNull().GreaterThan(0);
    }
}