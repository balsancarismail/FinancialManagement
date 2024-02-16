using Application.Features.FinancialTransaction.Constants;
using Core.CrossCuttingConcerns.Exceptions.Types;

namespace Application.Features.FinancialTransaction.Rules;

public class FinancialTransactionBusinessRules
{
    public Task FinancialTransactionMustNotBeNull(Domain.Entities.FinancialTransaction financialTransaction)
    {
        if (financialTransaction == null)
            throw new BusinessException(FinancialTransactionMessages.FinancialTransactionMustNotBeNull);

        return Task.CompletedTask;
    }

    public Task CategoryMustNotBeNull(Domain.Entities.Category category)
    {
        if (category == null) throw new BusinessException(FinancialTransactionMessages.CategoryMustNotBeNull);

        return Task.CompletedTask;
    }

    public Task UserIdOrCategoryIdMustBeExists(int? userId, int? categoryId)
    {
        if (!userId.HasValue && !categoryId.HasValue)
            throw new BusinessException(FinancialTransactionMessages.UserIdOrCategoryIdMustBeExists);

        return Task.CompletedTask;
    }
}