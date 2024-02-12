using FluentValidation;

namespace Application.Features.Budget.Commands.Create;

public class CreateBudgetCommandValidator : AbstractValidator<CreateBudgetCommand>
{
    public CreateBudgetCommandValidator()
    {
        RuleFor(
                c => c.StartDate)
            .NotNull().NotEmpty().LessThan(c => c.EndDate);
        RuleFor(c => c.EndDate).NotNull().NotEmpty();
    }
}