using Application.Services.Repositories;
using Domain.Entities;

namespace Application.Services.CategoryService;

public class CategoryManager(ICategoryRepository categoryRepository) : ICategoryService
{
    public async Task<Category> GetCategoryByIdAsync(int categoryId, CancellationToken cancellationToken)
    {
        return await categoryRepository.GetAsync(c => c.Id == categoryId, cancellationToken: cancellationToken);
    }
}