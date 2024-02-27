using System.Text.Json.Serialization;
using Application.Features.Budget.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Auth.Constants.ConstantRoles;

namespace Application.Features.Budget.Commands.Update;

public class UpdateBudgetCommand : IRequest<UpdateBudgetResponse>, ICacheRemoverRequest, ISecuredRequest,
    ITransactionalRequest, ILoggableRequest
{
    [JsonIgnore] public int Id { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    [JsonIgnore] public string CacheKey => "";

    [JsonIgnore] public bool BypassCache { get; }

    [JsonIgnore] public string CacheGroupKey => "GetBudget";

    [JsonIgnore] public string[] Roles => new[] { USER, ACCOUNTANT };

    public class UpdateBudgetCommandHandler(
        IMapper mapper,
        IBudgetRepository budgetRepository) : IRequestHandler<UpdateBudgetCommand, UpdateBudgetResponse>
    {
        public async Task<UpdateBudgetResponse> Handle(UpdateBudgetCommand request, CancellationToken cancellationToken)
        {
            var budget = mapper.Map<Domain.Entities.Budget>(request);
            var budgetEntity =
                await budgetRepository.GetAsync(b => b.Id == request.Id, cancellationToken: cancellationToken);

            mapper.Map(budget, budgetEntity);
            await budgetRepository.UpdateAsync(budgetEntity);

            return mapper.Map<UpdateBudgetResponse>(budget);
        }
    }
}