using Application.Features.Budget.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;

namespace Application.Features.Budget.Rules;

public class BudgetBusinessRules
{
    private readonly IBudgetRepository _budgetRepository;

    public BudgetBusinessRules(IBudgetRepository budgetRepository)
    {
        _budgetRepository = budgetRepository;
    }

    public async Task IsBudgetExists(int id, CancellationToken cancellationToken)
    {
        var budget = await _budgetRepository.GetAsync(b => b.Id == id, enableTracking: false,
            cancellationToken: cancellationToken);
        if (budget == null) throw new BusinessException(BudgetMessages.BudgetNotFound);
    }
}