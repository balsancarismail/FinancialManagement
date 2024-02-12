using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class CategoryRepository(BaseDbContext context)
    : EfRepositoryBase<Category, int, BaseDbContext>(context), ICategoryRepository
{
}