using Application.Features.Category.Constants;
using Core.CrossCuttingConcerns.Exceptions.Types;

namespace Application.Features.Category.Rules;

public class CategoryBusinessRules : ICategoryBusinessRules
{
    public Task CategoryMustNotBeNull(Domain.Entities.Category category)
    {
        if (category == null) throw new BusinessException(CategoryMessages.CategoryNotFound);

        return Task.CompletedTask;
    }
}
