using Application.Features.BudgetCategory.Commands.Create;
using FluentValidation;

namespace Application.Features.BudgetCategory.Commands.Delete;

public class DeleteBudgetCategoryCommandValidator : AbstractValidator<DeleteBudgetCategoryCommand>
{
    public DeleteBudgetCategoryCommandValidator()
    {
        RuleFor(
                c => c.Id)
            .NotNull().NotEmpty().GreaterThan(0);
    }
}