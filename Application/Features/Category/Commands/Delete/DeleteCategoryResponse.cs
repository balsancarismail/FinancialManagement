using Domain.Enums;

namespace Application.Features.Category.Commands.Delete;

public class DeleteCategoryResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public CategoryType CategoryType { get; set; }
}