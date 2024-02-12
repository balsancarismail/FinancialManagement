using Application.Features.BudgetCategory.Constants;
using Core.CrossCuttingConcerns.Exceptions.Types;

namespace Application.Features.BudgetCategory.Rules;

public class BudgetCategoryBusinessRules
{
    public void CategoryMustBeExists(Domain.Entities.Category category)
    {
        // Check if category exists
        if (category is null) throw new BusinessException(BudgetCategoryMessages.CategoryNotFound);
    }

    public void BudgetMustBeExists(Domain.Entities.Budget budget)
    {
        // Check if category exists
        if (budget is null) throw new BusinessException(BudgetCategoryMessages.BudgetNotFound);
    }

    public void BudgetCategoryMustBeExists(Domain.Entities.BudgetCategory budgetCategory)
    {
        if (budgetCategory is null) throw new BusinessException(BudgetCategoryMessages.BudgetCategoryNotFound);
    }
}