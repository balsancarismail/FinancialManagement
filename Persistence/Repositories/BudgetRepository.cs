using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class BudgetRepository(BaseDbContext context)
    : EfRepositoryBase<Budget, int, BaseDbContext>(context), IBudgetRepository
{
}