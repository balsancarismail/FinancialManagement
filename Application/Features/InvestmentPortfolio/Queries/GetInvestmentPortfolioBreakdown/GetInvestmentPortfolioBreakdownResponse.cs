namespace Application.Features.InvestmentPortfolio.Queries.GetInvestmentPortfolioBreakdown;

public class GetInvestmentPortfolioBreakdownResponse
{
    public IList<InvestmentBreakdownDto> InvestmentBreakdownDtos { get; set; }
    public string UserName { get; set; }
    public decimal TotalInvestedAmount { get; set; }
    public decimal TotalCurrentValue { get; set; }
    public decimal TotalProfit { get; set; }
}