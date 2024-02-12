using System.Text.Json.Serialization;
using Application.Features.BudgetCategory.Commands.Create;
using Application.Features.BudgetCategory.Rules;
using Application.Services.CategoryService;
using Application.Services.Repositories;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Auth.Constants.ConstantRoles;


namespace Application.Features.BudgetCategory.Commands.Update;

public class UpdateBudgetCategoryCommand : IRequest<UpdateBudgetCategoryResponse>, ICacheRemoverRequest, ISecuredRequest, ITransactionalRequest, ILoggableRequest
{
    [JsonIgnore]
    public string CacheKey => $"";
    [JsonIgnore]
    public bool BypassCache { get; }
    [JsonIgnore]
    public string CacheGroupKey => $"GetBudgetCategory";
    [JsonIgnore]
    public string[] Roles => new string[] { USER };
    [JsonIgnore]
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public decimal AllocatedAmount { get; set; }

    public class
        UpdateBudgetCategoryCommandHandler(
            IBudgetCategoryRepository budgetCategoryRepository,
            ICategoryService categoryService,
            BudgetCategoryBusinessRules budgetCategoryBusinessRules)
        : IRequestHandler<UpdateBudgetCategoryCommand, UpdateBudgetCategoryResponse>
    {
        public async Task<UpdateBudgetCategoryResponse> Handle(UpdateBudgetCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var category = await categoryService.GetCategoryByIdAsync(request.CategoryId, cancellationToken);
            var budgetCategory = await budgetCategoryRepository.GetAsync(predicate: b=> b.Id == request.Id, cancellationToken: cancellationToken);

            budgetCategoryBusinessRules.CategoryMustBeExists(category);
            budgetCategoryBusinessRules.BudgetCategoryMustBeExists(budgetCategory);

            budgetCategory.AllocatedAmount = request.AllocatedAmount;
            budgetCategory.Category = category;

            await budgetCategoryRepository.AddAsync(budgetCategory);

            return new UpdateBudgetCategoryResponse()
            {
                BudgetId = budgetCategory.BudgetId,
                CategoryId = budgetCategory.CategoryId,
                AllocatedAmount = budgetCategory.AllocatedAmount
            };
        }
    }

}