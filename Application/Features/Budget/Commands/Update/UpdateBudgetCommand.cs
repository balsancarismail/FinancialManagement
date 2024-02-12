using Application.Features.Budget.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using System.Text.Json.Serialization;
using static Application.Features.Auth.Constants.ConstantRoles;
namespace Application.Features.Budget.Commands.Update;

public class UpdateBudgetCommand : IRequest<UpdateBudgetResponse>, ICacheRemoverRequest, ISecuredRequest, ITransactionalRequest, ILoggableRequest
{
    [JsonIgnore]
    public string CacheKey => $"";
    [JsonIgnore]
    public bool BypassCache { get; }
    [JsonIgnore]
    public string CacheGroupKey => $"GetBudget";
    [JsonIgnore]
    public string[] Roles => new string[] { USER };
    [JsonIgnore]
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public class UpdateBudgetCommandHandler : IRequestHandler<UpdateBudgetCommand, UpdateBudgetResponse>
    {

        private readonly IMapper _mapper;
        private readonly IBudgetRepository _budgetRepository;

        public UpdateBudgetCommandHandler(IMapper mapper, IBudgetRepository budgetRepository, BudgetBusinessRules budgetBusinessRules)
        {
            _mapper = mapper;
            _budgetRepository = budgetRepository;
        }

        public async Task<UpdateBudgetResponse> Handle(UpdateBudgetCommand request, CancellationToken cancellationToken)
        {
            var budget = _mapper.Map<Domain.Entities.Budget>(request);
            var budgetEntity = await _budgetRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);

            _mapper.Map(budget, budgetEntity);
            await _budgetRepository.UpdateAsync(budgetEntity);

            return _mapper.Map<UpdateBudgetResponse>(budget);
        }
    }
}