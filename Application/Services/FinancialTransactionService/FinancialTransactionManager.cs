using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.FinancialTransactionService;

public class FinancialTransactionManager(IFinancialTransactionRepository financialTransactionRepository)
    : IFinancialTransactionService
{
    public async Task<Paginate<FinancialTransaction>> GetFinancialTransactionByAppUserIdAndTimesAsync(int appUserId,
        CancellationToken cancellationToken, DateTime sDateTime, DateTime eDateTime)
    {
        var financialTransactions = await financialTransactionRepository.GetListAsync(
            x => x.AppUserId == appUserId && x.Date >= sDateTime && x.Date <= eDateTime,
            cancellationToken: cancellationToken,
            include: x => x.Include(y => y.Category),
            enableTracking: false
        );

        return financialTransactions;
    }
}