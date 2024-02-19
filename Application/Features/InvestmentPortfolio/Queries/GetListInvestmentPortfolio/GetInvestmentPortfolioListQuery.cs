using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Requests;
using Core.Application.Responses;
using MediatR;

namespace Application.Features.InvestmentPortfolio.Queries.GetListInvestmentPortfolio;

public class GetInvestmentPortfolioListQuery : IRequest<GetListResponse<GetListInvestmentPortfolioListItemDto>>,
    ICachableRequest, ILoggableRequest
{
    public PageRequest PageRequest { get; init; }

    public string CacheKey => $"GetInvestmentPortfolioList({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetInvestmentPortfolio";
    public TimeSpan? SlidingExpiration { get; init; }
    public bool BypassCache { get; set; }

    public class GetInvestmentPortfolioListQueryHandler(
        IInvestmentPortfolioRepository investmentPortfolioRepository,
        IMapper mapper)
        : IRequestHandler<GetInvestmentPortfolioListQuery,
            GetListResponse<GetListInvestmentPortfolioListItemDto>>
    {
        public async Task<GetListResponse<GetListInvestmentPortfolioListItemDto>> Handle(
            GetInvestmentPortfolioListQuery request, CancellationToken cancellationToken)
        {
            var investmentPortfolios = await investmentPortfolioRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            return mapper.Map<GetListResponse<GetListInvestmentPortfolioListItemDto>>(investmentPortfolios);
        }
    }
}