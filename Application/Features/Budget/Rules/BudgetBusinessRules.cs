using Application.Features.Budget.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Persistence.Paging;

namespace Application.Features.Budget.Rules;

public class BudgetBusinessRules(IBudgetRepository budgetRepository)
{
    public async Task IsBudgetExists(int id, CancellationToken cancellationToken)
    {
        var budget = await budgetRepository.GetAsync(b => b.Id == id, enableTracking: false,
            cancellationToken: cancellationToken);
        if (budget == null) throw new BusinessException(BudgetMessages.BudgetNotFound);
    }

    public void BudgeMustBeExists(Domain.Entities.Budget budget)
    {
        if (budget == null) throw new BusinessException(BudgetMessages.BudgetNotFound);
    }

    public void FinancialTransactionDataMustBeExists(
        Paginate<Domain.Entities.FinancialTransaction> financialTransactionPaginate)
    {
        if (financialTransactionPaginate is null || !financialTransactionPaginate.Items.Any())
            throw new BusinessException(BudgetMessages.FinancialTransactionNotFound);
    }
}