using System.Text.Json.Serialization;
using Application.Features.BudgetCategory.Rules;
using Application.Services.BudgetService;
using Application.Services.CategoryService;
using Application.Services.Repositories;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Auth.Constants.ConstantRoles;


namespace Application.Features.BudgetCategory.Commands.Delete;

/// <summary>
///     Represents a command to delete a budget category.
/// </summary>
public class DeleteBudgetCategoryCommand : IRequest<DeleteBudgetCategoryResponse>, ICacheRemoverRequest,
    ISecuredRequest, ITransactionalRequest, ILoggableRequest
{
    [JsonIgnore] public int Id { get; set; }

    [JsonIgnore] public string CacheKey => "";

    [JsonIgnore] public bool BypassCache { get; }

    [JsonIgnore] public string CacheGroupKey => "GetBudgetCategory";

    [JsonIgnore] public string[] Roles => new[] { USER };

    /// <summary>
    ///     Represents a handler for the DeleteBudgetCategoryCommand.
    /// </summary>
    public class DeleteBudgetCategoryCommandHandler(
        IBudgetCategoryRepository budgetCategoryRepository,
        IBudgetService budgetService,
        ICategoryService categoryService,
        BudgetCategoryBusinessRules budgetCategoryBusinessRules)
        : IRequestHandler<DeleteBudgetCategoryCommand, DeleteBudgetCategoryResponse>
    {
        private readonly IBudgetService _budgetService = budgetService;
        private readonly ICategoryService _categoryService = categoryService;

        /// <summary>
        ///     Handles the DeleteBudgetCategoryCommand.
        ///     Deletes a budget category.
        /// </summary>
        /// <param name="request">The DeleteBudgetCategoryCommand request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation and contains the DeleteBudgetCategoryResponse.</returns>
        public async Task<DeleteBudgetCategoryResponse> Handle(DeleteBudgetCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var budgetCategory =
                await budgetCategoryRepository.GetAsync(b => b.Id == request.Id, cancellationToken: cancellationToken);

            budgetCategoryBusinessRules.BudgetCategoryMustBeExists(budgetCategory);

            await budgetCategoryRepository.DeleteAsync(budgetCategory);

            return new DeleteBudgetCategoryResponse
            {
                BudgetId = budgetCategory.BudgetId,
                CategoryId = budgetCategory.CategoryId,
                AllocatedAmount = budgetCategory.AllocatedAmount
            };
        }
    }
}