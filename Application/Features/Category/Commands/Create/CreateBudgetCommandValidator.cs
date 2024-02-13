using FluentValidation;

namespace Application.Features.Category.Commands.Create;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(c => c.Name).NotNull().NotEmpty();
        RuleFor(c => c.CategoryType).NotNull().NotEmpty();
    }
}