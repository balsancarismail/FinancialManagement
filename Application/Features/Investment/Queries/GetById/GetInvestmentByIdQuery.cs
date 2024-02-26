using Application.Features.Investment.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Features.Auth.Constants.ConstantRoles;

namespace Application.Features.Investment.Queries.GetById;

public class GetInvestmentByIdQuery : IRequest<GetInvestmentByIdResponse>, ILoggableRequest,
    ICachableRequest,
    ISecuredRequest
{
    public int Id { get; set; }

    public string CacheKey => $"GetInvestmentById({Id})";
    public bool BypassCache { get; }
    public string CacheGroupKey => "GetInvestment";
    public TimeSpan? SlidingExpiration { get; init; }
    public string[] Roles => new[] { USER, FINANCIALANALYST };

    public class GetInvestmentByIdQueryHandler(
        IInvestmentRepository investmentRepository,
        IMapper mapper,
        InvestmentBusinessRules investmentBusinessRules)
        : IRequestHandler<GetInvestmentByIdQuery, GetInvestmentByIdResponse>
    {
        public async Task<GetInvestmentByIdResponse> Handle(GetInvestmentByIdQuery request,
            CancellationToken cancellationToken)
        {
            var investment = await investmentRepository.GetAsync(b => b.Id == request.Id, enableTracking: false,
                cancellationToken: cancellationToken, include: src => src.Include(b => b.Portfolio));
            investmentBusinessRules.InvestmentMustBeExists(investment);

            return mapper.Map<GetInvestmentByIdResponse>(investment);
        }
    }
}