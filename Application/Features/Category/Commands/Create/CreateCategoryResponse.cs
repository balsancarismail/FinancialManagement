using System.Text.Json.Serialization;
using Domain.Enums;

namespace Application.Features.Category.Commands.Create;

public class CreateCategoryResponse
{
    public int Id { get; set; }
    public string Name { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public CategoryType CategoryType { get; set; }
}