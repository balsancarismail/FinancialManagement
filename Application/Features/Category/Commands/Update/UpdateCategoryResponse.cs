using Domain.Enums;

namespace Application.Features.Category.Commands.Update;

public class UpdateCategoryResponse
{
    public string Name { get; set; }
    public CategoryType CategoryType { get; set; }
}