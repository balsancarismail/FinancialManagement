using Domain.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Text.Json.Serialization;

namespace Application.Features.Category.Commands.Create;

public class CreateCategoryResponse
{
    public int Id { get; set; }
    public string Name { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public CategoryType CategoryType { get; set; }
}