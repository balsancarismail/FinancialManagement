using FluentValidation;

namespace Application.Features.Budget.Commands.Update;

public class UpdateBudgetCommandValidator : AbstractValidator<UpdateBudgetCommand>
{
    public UpdateBudgetCommandValidator()
    {
        RuleFor(c => c.Id).NotNull().GreaterThan(0);
        RuleFor(
                c => c.StartDate)
            .NotNull().NotEmpty().LessThan(c => c.EndDate);
        RuleFor(c => c.EndDate).NotNull().NotEmpty();
    }
}