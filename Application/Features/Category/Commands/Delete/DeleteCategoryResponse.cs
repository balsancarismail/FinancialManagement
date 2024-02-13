using Domain.Enums;

namespace Application.Features.Category.Commands.Delete;

public class DeleteCategoryResponse
{
    public string Name { get; set; }
    public CategoryType CategoryType { get; set; }
}