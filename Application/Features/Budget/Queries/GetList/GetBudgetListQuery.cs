using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Requests;
using Core.Application.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Budget.Queries.GetList;

public class GetBudgetListQuery : IRequest<GetListResponse<GetListBudgetListItemDto>>, ILoggableRequest,
    ICachableRequest
{
    public PageRequest PageRequest { get; set; }
    public string CacheKey => $"GetBudgetList({PageRequest.PageIndex},{PageRequest.PageSize})";
    public bool BypassCache { get; }
    public string CacheGroupKey => "GetBudget";
    public TimeSpan? SlidingExpiration { get; init; }

    public class
        GetBudgetListQueryHandler(IBudgetRepository budgetRepository, IMapper mapper)
        : IRequestHandler<GetBudgetListQuery, GetListResponse<GetListBudgetListItemDto>>
    {
        public async Task<GetListResponse<GetListBudgetListItemDto>> Handle(GetBudgetListQuery request,
            CancellationToken cancellationToken)
        {
            var budgets = await budgetRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                include: b => b.Include(src => src.AppUser),
                enableTracking: false);

            var budgetList = mapper.Map<GetListResponse<GetListBudgetListItemDto>>(budgets);

            return budgetList;
        }
    }
}