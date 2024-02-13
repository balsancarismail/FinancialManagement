using Domain.Enums;
using System.Text.Json.Serialization;

namespace Application.Features.Category.Commands.Delete;

public class DeleteCategoryResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public CategoryType CategoryType { get; set; }
}