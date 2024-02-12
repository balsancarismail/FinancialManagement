using FluentValidation;

namespace Application.Features.BudgetCategory.Commands.Create;

public class CreateBudgetCategoryCommandValidator : AbstractValidator<CreateBudgetCategoryCommand>
{
    public CreateBudgetCategoryCommandValidator()
    {
        RuleFor(
                c => c.AllocatedAmount)
            .NotNull().NotEmpty().GreaterThan(0);
    }
}