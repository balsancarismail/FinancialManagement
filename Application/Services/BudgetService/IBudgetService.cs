using Budget = Domain.Entities.Budget;

namespace Application.Services.BudgetService;

public interface IBudgetService
{
    Task<Budget> GetBudgetByIdAsync(int budgetId, CancellationToken cancellationToken);
}