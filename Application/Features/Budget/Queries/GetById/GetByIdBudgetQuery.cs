using Application.Features.Budget.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Features.Auth.Constants.ConstantRoles;

namespace Application.Features.Budget.Queries.GetById;

public class GetByIdBudgetQuery : IRequest<GetByIdBudgetResponse>, ILoggableRequest, ICachableRequest, ISecuredRequest
{
    public int Id { get; set; }

    public string CacheKey => $"GetBudgetById({Id})";
    public bool BypassCache { get; }
    public string CacheGroupKey => "GetBudget";
    public TimeSpan? SlidingExpiration { get; init; }

    public string[] Roles => new[] { ACCOUNTANT, USER };

    public class GetByIdBudgetQueryHandler(
        IBudgetRepository budgetRepository,
        IMapper mapper,
        IBudgetBusinessRules budgetBusinessRules)
        : IRequestHandler<GetByIdBudgetQuery, GetByIdBudgetResponse>
    {
        public async Task<GetByIdBudgetResponse> Handle(GetByIdBudgetQuery request, CancellationToken cancellationToken)
        {
            var budget = await budgetRepository.GetAsync(b => b.Id == request.Id,
                b => b.Include(b => b.AppUser),
                enableTracking: false,
                cancellationToken: cancellationToken);
            await budgetBusinessRules.IsBudgetExists(request.Id, cancellationToken);

            return mapper.Map<GetByIdBudgetResponse>(budget);
        }
    }
}