using Domain.Entities;

namespace Application.Services.CategoryService;

public interface ICategoryService
{
    Task<Category> GetCategoryByIdAsync(int categoryId, CancellationToken cancellationToken);
}