using Application.Features.BudgetCategory.Commands.Create;
using FluentValidation;

namespace Application.Features.BudgetCategory.Commands.Update;

public class UpdateBudgetCategoryCommandValidator : AbstractValidator<UpdateBudgetCategoryCommand>
{
    public UpdateBudgetCategoryCommandValidator()
    {
        RuleFor(
                c => c.AllocatedAmount)
            .NotNull().NotEmpty().GreaterThan(0);
    }
}