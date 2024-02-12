﻿using Application.Features.Budget.Commands.Update;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using System.Text.Json.Serialization;
using Application.Features.Budget.Rules;
using Application.Services.Repositories;
using static Application.Features.Auth.Constants.ConstantRoles;

namespace Application.Features.Budget.Commands.Delete;

public class DeleteBudgetCommand : IRequest<DeleteBudgetResponse>, IRequest<UpdateBudgetResponse>, ICacheRemoverRequest, ISecuredRequest, ITransactionalRequest, ILoggableRequest
{
    [JsonIgnore]
    public string CacheKey => $"";
    [JsonIgnore]
    public bool BypassCache { get; }
    [JsonIgnore]
    public string CacheGroupKey => $"GetBudget";
    [JsonIgnore]
    public string[] Roles => new string[] { USER };
    public int Id { get; set; }

    public class DeleteBudgetCommandHandler(IBudgetRepository budgetRepository, BudgetBusinessRules budgetBusinessRules)
        : IRequestHandler<DeleteBudgetCommand, DeleteBudgetResponse>
    {
        public async Task<DeleteBudgetResponse> Handle(DeleteBudgetCommand request, CancellationToken cancellationToken)
        {
            await budgetBusinessRules.IsBudgetExists(request.Id, cancellationToken);
            var budgetEntity = await budgetRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);
            await budgetRepository.DeleteAsync(budgetEntity);
            return new DeleteBudgetResponse();
        }
    }
}