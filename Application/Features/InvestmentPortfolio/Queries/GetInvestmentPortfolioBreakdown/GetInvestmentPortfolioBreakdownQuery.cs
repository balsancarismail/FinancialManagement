using System.Text.Json.Serialization;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Features.Auth.Constants.ConstantRoles;

namespace Application.Features.InvestmentPortfolio.Queries.GetInvestmentPortfolioBreakdown;

public class GetInvestmentPortfolioBreakdownQuery : IRequest<GetInvestmentPortfolioBreakdownResponse>, ICachableRequest,
    ILoggableRequest, ISecuredRequest
{
    public int Id { get; set; }
    public string CacheKey => $"GetUserBreakdown({Id})";
    public bool BypassCache { get; }
    public string CacheGroupKey => "GetUserBreakdown";
    public TimeSpan? SlidingExpiration { get; init; }
    [JsonIgnore] public string[] Roles => new[] { FINANCIALANALYST };


    public class GetUserBreakdownQueryHandler(
        IInvestmentPortfolioRepository investmentPortfolioRepository)
        : IRequestHandler<GetInvestmentPortfolioBreakdownQuery,
            GetInvestmentPortfolioBreakdownResponse>
    {
        public async Task<GetInvestmentPortfolioBreakdownResponse> Handle(GetInvestmentPortfolioBreakdownQuery request,
            CancellationToken cancellationToken)
        {
            var investmentPortfolio = await investmentPortfolioRepository.GetAsync(
                x => x.Id == request.Id,
                x => x.Include(y => y.Investments).Include(b => b.AppUser),
                cancellationToken: cancellationToken,
                enableTracking: false
            );

            var investmentBreakdownDto = AdjustInvestments(investmentPortfolio);
            var response = new GetInvestmentPortfolioBreakdownResponse
            {
                InvestmentBreakdownDtos = investmentBreakdownDto,
                UserName = investmentPortfolio.AppUser.UserName!,
                TotalInvestedAmount = investmentBreakdownDto.Sum(x => x.Amount),
                TotalCurrentValue = investmentBreakdownDto.Sum(x => x.RandomProfitOrLoss),
                TotalProfit = investmentBreakdownDto.Sum(x => x.RandomProfitOrLoss - x.Amount)
            };

            return response;
        }

        private IList<InvestmentBreakdownDto> AdjustInvestments(Domain.Entities.InvestmentPortfolio investmentPortfolio)
        {
            return investmentPortfolio.Investments.Select(x => new InvestmentBreakdownDto
            {
                PortfolioId = x.PortfolioId,
                InvestmentType = x.InvestmentType,
                Amount = x.Amount,
                RandomProfitOrLoss = x.RandomProfitOrLoss,
                PurchaseDate = x.PurchaseDate
            }).ToList();
        }
    }
}