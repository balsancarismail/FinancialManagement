using FluentValidation;

namespace Application.Features.Category.Commands.Update;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(c => c.Name).NotNull().NotEmpty();
        RuleFor(c => c.CategoryType).NotNull().NotEmpty();
    }
}