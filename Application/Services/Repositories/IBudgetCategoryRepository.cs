using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IBudgetCategoryRepository : IAsyncRepository<BudgetCategory, int>
{
}