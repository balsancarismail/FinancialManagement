using System.Text.Json.Serialization;
using Domain.Enums;

namespace Application.Features.Category.Queries.GetList;

public class GetListCategoryListItemDto
{
    public int Id { get; set; }
    public string Name { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public CategoryType CategoryType { get; set; }
}