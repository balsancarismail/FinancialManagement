using Core.Persistence.Paging;

namespace Application.Features.Budget.Rules;

public interface IBudgetBusinessRules
{
    Task IsBudgetExists(int id, CancellationToken cancellationToken);
    void BudgeMustBeExists(Domain.Entities.Budget budget);
    void FinancialTransactionDataMustBeExists(Paginate<Domain.Entities.FinancialTransaction> financialTransactionPaginate);
}