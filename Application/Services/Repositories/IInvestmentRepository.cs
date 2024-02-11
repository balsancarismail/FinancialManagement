using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IInvestmentRepository : IAsyncRepository<Investment, int>
{
}