using System.Text.Json.Serialization;
using Domain.Enums;

namespace Application.Features.Investment.Commands.Update;

public class UpdateInvestmentResponse
{
    public int Id { get; set; }
    public int PortfolioId { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public InvestmentType InvestmentType { get; set; } // e.g., Stock, Bond

    public decimal Amount { get; set; }
    public DateTime PurchaseDate { get; set; }
}