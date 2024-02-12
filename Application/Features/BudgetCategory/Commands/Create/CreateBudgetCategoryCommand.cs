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


namespace Application.Features.BudgetCategory.Commands.Create;

public class CreateBudgetCategoryCommand : IRequest<CreateBudgetCategoryResponse>, ICacheRemoverRequest,
    ISecuredRequest, ITransactionalRequest, ILoggableRequest
{
    public int CategoryId { get; set; }
    public int BudgetId { get; set; }
    public decimal AllocatedAmount { get; set; }

    [JsonIgnore] public string CacheKey => "";

    [JsonIgnore] public bool BypassCache { get; }

    [JsonIgnore] public string CacheGroupKey => "GetBudgetCategory";

    [JsonIgnore] public string[] Roles => new[] { USER };

    public class
        CreateBudgetCategoryCommandHandler(
            IBudgetCategoryRepository budgetCategoryRepository,
            IBudgetService budgetService,
            ICategoryService categoryService,
            BudgetCategoryBusinessRules budgetCategoryBusinessRules)
        : IRequestHandler<CreateBudgetCategoryCommand, CreateBudgetCategoryResponse>
    {
        public async Task<CreateBudgetCategoryResponse> Handle(CreateBudgetCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var budget = await budgetService.GetBudgetByIdAsync(request.BudgetId, cancellationToken);
            var category = await categoryService.GetCategoryByIdAsync(request.CategoryId, cancellationToken);

            budgetCategoryBusinessRules.BudgetMustBeExists(budget);
            budgetCategoryBusinessRules.CategoryMustBeExists(category);

            var budgetCategory = new Domain.Entities.BudgetCategory
            {
                Budget = budget,
                Category = category,
                AllocatedAmount = request.AllocatedAmount
            };

            await budgetCategoryRepository.AddAsync(budgetCategory);
            return new CreateBudgetCategoryResponse();
        }
    }
}