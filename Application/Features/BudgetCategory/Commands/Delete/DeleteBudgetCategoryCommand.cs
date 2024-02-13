using System.Text.Json.Serialization;
using Application.Features.BudgetCategory.Rules;
using Application.Services.Repositories;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Auth.Constants.ConstantRoles;


namespace Application.Features.BudgetCategory.Commands.Delete;

public class DeleteBudgetCategoryCommand : IRequest<DeleteBudgetCategoryResponse>, ICacheRemoverRequest,
    ISecuredRequest, ITransactionalRequest, ILoggableRequest
{
    [JsonIgnore] public int Id { get; set; }

    [JsonIgnore] public string CacheKey => "";

    [JsonIgnore] public bool BypassCache { get; }

    [JsonIgnore] public string CacheGroupKey => "GetBudgetCategory";

    [JsonIgnore] public string[] Roles => new[] { USER };

    public class DeleteBudgetCategoryCommandHandler(
        IBudgetCategoryRepository budgetCategoryRepository,
        BudgetCategoryBusinessRules budgetCategoryBusinessRules)
        : IRequestHandler<DeleteBudgetCategoryCommand, DeleteBudgetCategoryResponse>
    {
        public async Task<DeleteBudgetCategoryResponse> Handle(DeleteBudgetCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var budgetCategory =
                await budgetCategoryRepository.GetAsync(b => b.Id == request.Id, cancellationToken: cancellationToken);

            budgetCategoryBusinessRules.BudgetCategoryMustBeExists(budgetCategory);

            await budgetCategoryRepository.DeleteAsync(budgetCategory);

            return new DeleteBudgetCategoryResponse
            {
                Id = budgetCategory.Id,
                BudgetId = budgetCategory.BudgetId,
                CategoryId = budgetCategory.CategoryId,
                AllocatedAmount = budgetCategory.AllocatedAmount
            };
        }
    }
}