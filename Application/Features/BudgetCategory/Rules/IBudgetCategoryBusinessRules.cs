namespace Application.Features.BudgetCategory.Rules;

public interface IBudgetCategoryBusinessRules
{
    void CategoryMustBeExists(Domain.Entities.Category category);
    void BudgetMustBeExists(Domain.Entities.Budget budget);
    void BudgetCategoryMustBeExists(Domain.Entities.BudgetCategory budgetCategory);
}