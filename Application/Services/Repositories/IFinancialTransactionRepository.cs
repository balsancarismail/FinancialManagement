using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IFinancialTransactionRepository : IAsyncRepository<FinancialTransaction, int>
{
}