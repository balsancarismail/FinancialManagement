using Application.Features.InvestmentPortfolio.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using MediatR;

namespace Application.Features.InvestmentPortfolio.Queries.GetById;

public class GetInvestmentPortfolioByIdQuery : IRequest<GetInvestmentPortfolioByIdResponse>, ICachableRequest,
    ILoggableRequest
{
    public int Id { get; set; }

    public string CacheKey => $"InvestmentPortfolio({Id})";
    public string CacheGroupKey => "InvestmentPortfolio";
    public TimeSpan? SlidingExpiration { get; init; }
    public bool BypassCache { get; set; }

    public class
        GetInvestmentPortfolioByIdQueryHandler(
            IInvestmentPortfolioRepository investmentPortfolioRepository,
            IMapper mapper,
            InvestmentPortfolioBusinessRules investmentPortfolioBusinessRules)
        : IRequestHandler<GetInvestmentPortfolioByIdQuery,
            GetInvestmentPortfolioByIdResponse>
    {
        public async Task<GetInvestmentPortfolioByIdResponse> Handle(GetInvestmentPortfolioByIdQuery request,
            CancellationToken cancellationToken)
        {
            var investmenPortfolio = await investmentPortfolioRepository.GetAsync(b => b.Id == request.Id,
                enableTracking: false, cancellationToken: cancellationToken);
            await investmentPortfolioBusinessRules.InvestmentPortfolioMustNotBeNull(investmenPortfolio);

            return mapper.Map<GetInvestmentPortfolioByIdResponse>(investmenPortfolio);
        }
    }
}