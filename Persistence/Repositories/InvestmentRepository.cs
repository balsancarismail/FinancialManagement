using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class InvestmentRepository(BaseDbContext context)
    : EfRepositoryBase<Investment, int, BaseDbContext>(context), IInvestmentRepository
{

}