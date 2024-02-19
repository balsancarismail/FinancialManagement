using Application.Features.Budget.Rules;
using Application.Services.FinancialTransactionService;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Security.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Budget.Queries.GetUserBreakdown;

public class GetBudgetBreakdownQuery : IRequest<GetBudgetBreakdownResponse>, ICachableRequest, ILoggableRequest
{
    public int Id { get; set; }
    public string CacheKey => $"GetUserBreakdown({Id})";
    public bool BypassCache { get; }
    public string CacheGroupKey => "GetUserBreakdown";
    public TimeSpan? SlidingExpiration { get; init; }

    public class GetUserBreakdownQueryHandler(IBudgetRepository budgetRepository, IFinancialTransactionService financialTransactionService, IMapper mapper, BudgetBusinessRules budgetBusinessRules,UserManager<AppUser> userManager)
        : IRequestHandler<GetBudgetBreakdownQuery, GetBudgetBreakdownResponse>
    {
        public async Task<GetBudgetBreakdownResponse> Handle(GetBudgetBreakdownQuery request,
            CancellationToken cancellationToken)
        {
            var budget = await budgetRepository.GetAsync(
                predicate: x => x.Id == request.Id,
                include: x => x.Include(y => y.BudgetCategories).ThenInclude(x=> x.Category).Include(b => b.AppUser),
                cancellationToken: cancellationToken,
                enableTracking: false
            );
            budgetBusinessRules.BudgeMustBeExists(budget);

            var financialTransactionPaginate = await financialTransactionService.GetFinancialTransactionByAppUserIdAndTimesAsync(budget.AppUserId, cancellationToken, budget.StartDate, budget.EndDate);
            budgetBusinessRules.FinancialTransactionDataMustBeExists(financialTransactionPaginate);

            var budgetCategoryBreakdownDtos = AdjustBudgetCategorySpends(budget.BudgetCategories, financialTransactionPaginate.Items,cancellationToken);
            var response = await AdjustBudgetSpend(budgetCategoryBreakdownDtos, cancellationToken,budget);

            return response;
        }

        private async Task<GetBudgetBreakdownResponse> AdjustBudgetSpend(IList<BudgetCategoryBreakdownDto> budgetCategoryBreakdownDtos, CancellationToken cancellationToken, Domain.Entities.Budget budget)
        {
            var response = new GetBudgetBreakdownResponse
            {
                BudgetCategoryBreakdownDtos = budgetCategoryBreakdownDtos,
                UserName = budget.AppUser.UserName!,
                TotalAimedAllocatedAmount = budget.BudgetCategories.Where(x => x.Category.CategoryType == CategoryType.Income).Sum(x => x.AllocatedAmount),
                TotalAllocatedAmount = budgetCategoryBreakdownDtos.Where(x => x.CategoryType == CategoryType.Income).Sum(x => x.OperationsTotal),
                TotalSpentAmount = budgetCategoryBreakdownDtos.Where(x => x.CategoryType == CategoryType.Expense).Sum(x => x.OperationsTotal),
                TotalAimedSpentAmount = budget.BudgetCategories.Where(x => x.Category.CategoryType == CategoryType.Expense).Sum(x => x.AllocatedAmount)
            };
            return response;
        }

        private IList<BudgetCategoryBreakdownDto> AdjustBudgetCategorySpends(ICollection<Domain.Entities.BudgetCategory> budgetBudgetCategories, IEnumerable<Domain.Entities.FinancialTransaction> items, CancellationToken cancellationToken)
        {

            IList< BudgetCategoryBreakdownDto> budgetCategoryBreakdownDtos = new List<BudgetCategoryBreakdownDto>();
            foreach (var budgetCategory in budgetBudgetCategories)
            {
                var dto = new BudgetCategoryBreakdownDto
                {
                    Id = budgetCategory.Id,
                    CategoryName = budgetCategory.Category.Name,
                    AllocatedAmount = budgetCategory.AllocatedAmount,
                    CategoryType = budgetCategory.Category.CategoryType
                };
                dto.OperationsTotal = items!.Where(x => x.CategoryId == budgetCategory.CategoryId).Sum(x => x.Amount);
                budgetCategoryBreakdownDtos.Add(dto);
            }
            return budgetCategoryBreakdownDtos;
        }
    }
}