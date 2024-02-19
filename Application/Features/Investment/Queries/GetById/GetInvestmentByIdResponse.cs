﻿using System.Text.Json.Serialization;
using Domain.Enums;

namespace Application.Features.Investment.Queries.GetById;

public class GetInvestmentByIdResponse
{
    public int Id { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public InvestmentType InvestmentType { get; set; } // e.g., Stock, Bond
    public decimal Amount { get; set; }
    public DateTime PurchaseDate { get; set; }
    public string? PortfolioName { get; set; }
}