using FluentValidation;

namespace Application.Features.Category.Commands.Delete;

public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator()
    {
        RuleFor(c => c.Id).NotNull().NotEmpty().GreaterThan(0);
    }
}