using Application.Features.Budget.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Budget.Queries.GetByıd;

public class GetByIdBudgetQuery : IRequest<GetByIdBudgetResponse>, ILoggableRequest, ICachableRequest
{
    public int Id { get; set; }

    public string CacheKey => $"GetBudgetById({Id})";
    public bool BypassCache { get; }
    public string CacheGroupKey => "GetBudget";
    public TimeSpan? SlidingExpiration { get; init; }

    public class GetByIdBudgetQueryHandler(
        IBudgetRepository budgetRepository,
        IMapper mapper,
        BudgetBusinessRules budgetBusinessRules)
        : IRequestHandler<GetByIdBudgetQuery, GetByIdBudgetResponse>
    {
        public async Task<GetByIdBudgetResponse> Handle(GetByIdBudgetQuery request, CancellationToken cancellationToken)
        {
            var budget = await budgetRepository.GetAsync(b => b.Id == request.Id,
                                include: b => b.Include(b => b.AppUser),
                                enableTracking: false,
                               cancellationToken: cancellationToken);
            await budgetBusinessRules.IsBudgetExists(request.Id, cancellationToken);

            return mapper.Map<GetByIdBudgetResponse>(budget);
        }
    }
}