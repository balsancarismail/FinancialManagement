using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class FinancialTransactionRepository(BaseDbContext context)
    : EfRepositoryBase<FinancialTransaction, int, BaseDbContext>(context), IFinancialTransactionRepository
{
}