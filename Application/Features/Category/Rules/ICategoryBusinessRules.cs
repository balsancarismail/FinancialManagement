namespace Application.Features.Category.Rules;

public interface ICategoryBusinessRules
{
    Task CategoryMustNotBeNull(Domain.Entities.Category category);
}