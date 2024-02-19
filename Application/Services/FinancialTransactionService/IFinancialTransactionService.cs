using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Services.FinancialTransactionService;

public interface IFinancialTransactionService
{
    Task<Paginate<FinancialTransaction>> GetFinancialTransactionByAppUserIdAndTimesAsync(int appUserId, CancellationToken cancellationToken, DateTime sDateTime, DateTime eDateTime);
}