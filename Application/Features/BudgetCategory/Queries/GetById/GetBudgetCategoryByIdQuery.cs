using Application.Features.BudgetCategory.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Features.Auth.Constants.ConstantRoles;

namespace Application.Features.BudgetCategory.Queries.GetById;

public class GetBudgetCategoryByIdQuery : IRequest<GetBudgetCategoryByIdResponse>, ILoggableRequest, ISecuredRequest, ICachableRequest
{
    public int Id { get; set; }

    public string CacheKey => $"GetBudgetCategoryById({Id})";
    public bool BypassCache { get; }
    public string CacheGroupKey => "GetBudgetCategory";
    public TimeSpan? SlidingExpiration { get; init; }
    public string[] Roles => new[] { USER, ACCOUNTANT };

    public class
        GetBudgetCategoryByIdQueryHandler(
            IBudgetCategoryRepository budgetCategoryRepository,
            IMapper mapper,
            BudgetCategoryBusinessRules budgetCategoryBusinessRules)
        : IRequestHandler<GetBudgetCategoryByIdQuery, GetBudgetCategoryByIdResponse>
    {
        public async Task<GetBudgetCategoryByIdResponse> Handle(GetBudgetCategoryByIdQuery request,
            CancellationToken cancellationToken)
        {
            var budgetCategory = await budgetCategoryRepository.GetAsync(b => b.Id == request.Id, enableTracking: false,
                cancellationToken: cancellationToken, include: b => b.Include(src => src.Category));

            budgetCategoryBusinessRules.BudgetCategoryMustBeExists(budgetCategory);
            return mapper.Map<GetBudgetCategoryByIdResponse>(budgetCategory);
        }
    }
}