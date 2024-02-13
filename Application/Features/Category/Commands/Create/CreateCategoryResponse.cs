using Domain.Enums;

namespace Application.Features.Category.Commands.Create;

public class CreateCategoryResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public CategoryType CategoryType { get; set; }
}