using System.Text.Json.Serialization;
using Application.Features.FinancialTransaction.Rules;
using Application.Services.CategoryService;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Auth.Constants.ConstantRoles;

namespace Application.Features.FinancialTransaction.Commands.Update;

public class UpdateFinancialTransactionCommand : IRequest<UpdateFinancialTransactionResponse>, ICacheRemoverRequest,
    ISecuredRequest,
    ITransactionalRequest, ILoggableRequest
{
    [JsonIgnore] public int Id { get; set; }

    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public int CategoryId { get; set; }
    public string Description { get; set; }

    [JsonIgnore] public string CacheKey => "";

    [JsonIgnore] public bool BypassCache { get; }

    [JsonIgnore] public string CacheGroupKey => "GetFinancialTransaction";

    [JsonIgnore] public string[] Roles => new[] { USER, ACCOUNTANT };

    public class
        UpdateFinancialTransactionCommandHandler(
            IMapper mapper,
            IFinancialTransactionRepository financialTransactionRepository,
            FinancialTransactionBusinessRules financialTransactionBusinessRules,
            ICategoryService categoryService)
        : IRequestHandler<UpdateFinancialTransactionCommand,
            UpdateFinancialTransactionResponse>
    {
        public async Task<UpdateFinancialTransactionResponse> Handle(UpdateFinancialTransactionCommand request,
            CancellationToken cancellationToken)
        {
            var financialTransactionEntity =
                await financialTransactionRepository.GetAsync(ft => ft.Id == request.Id,
                    cancellationToken: cancellationToken);
            await financialTransactionBusinessRules.FinancialTransactionMustNotBeNull(financialTransactionEntity);

            var financialTransaction = mapper.Map<Domain.Entities.FinancialTransaction>(request);
            mapper.Map(financialTransaction, financialTransactionEntity);

            var category = await categoryService.GetCategoryByIdAsync(request.CategoryId, cancellationToken);
            await financialTransactionBusinessRules.CategoryMustNotBeNull(category);

            financialTransactionEntity.Category = category;

            await financialTransactionRepository.UpdateAsync(financialTransactionEntity);

            return mapper.Map<UpdateFinancialTransactionResponse>(financialTransactionEntity);
        }
    }
}