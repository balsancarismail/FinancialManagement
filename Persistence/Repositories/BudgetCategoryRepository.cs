using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class BudgetCategoryRepository(BaseDbContext context)
    : EfRepositoryBase<BudgetCategory, int, BaseDbContext>(context), IBudgetCategoryRepository
{
}