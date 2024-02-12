using Application.Services.Repositories;
using Domain.Entities;

namespace Application.Services.BudgetService;

public class BudgetManager(IBudgetRepository budgetRepository) : IBudgetService
{
    public async Task<Budget> GetBudgetByIdAsync(int budgetId, CancellationToken cancellationToken)
    {
        return await budgetRepository.GetAsync(predicate: b => b.Id == budgetId, cancellationToken: cancellationToken);
    }
}