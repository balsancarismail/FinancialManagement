namespace Application.Features.FinancialTransaction.Rules;

public interface IFinancialTransactionBusinessRules
{
    Task FinancialTransactionMustNotBeNull(Domain.Entities.FinancialTransaction financialTransaction);
    Task CategoryMustNotBeNull(Domain.Entities.Category category);
    Task UserIdOrCategoryIdMustBeExists(int? userId, int? categoryId);
}