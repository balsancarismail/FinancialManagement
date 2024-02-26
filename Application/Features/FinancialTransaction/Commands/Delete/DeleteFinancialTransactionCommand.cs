using System.Text.Json.Serialization;
using Application.Features.FinancialTransaction.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Auth.Constants.ConstantRoles;

namespace Application.Features.FinancialTransaction.Commands.Delete;

public class DeleteFinancialTransactionCommand : IRequest<DeleteFinancialTransactionResponse>, ICacheRemoverRequest,
    ISecuredRequest,
    ITransactionalRequest, ILoggableRequest
{
    [JsonIgnore] public int Id { get; set; }

    [JsonIgnore] public string CacheKey => "";

    [JsonIgnore] public bool BypassCache { get; }

    [JsonIgnore] public string CacheGroupKey => "GetFinancialTransaction";

    [JsonIgnore] public string[] Roles => new[] { USER, ACCOUNTANT };

    public class
        DeleteFinancialTransactionCommandHandler(
            IMapper mapper,
            IFinancialTransactionRepository financialTransactionRepository,
            FinancialTransactionBusinessRules financialTransactionBusinessRules)
        : IRequestHandler<DeleteFinancialTransactionCommand,
            DeleteFinancialTransactionResponse>
    {
        public async Task<DeleteFinancialTransactionResponse> Handle(DeleteFinancialTransactionCommand request,
            CancellationToken cancellationToken)
        {
            var financialTransactionEntity =
                await financialTransactionRepository.GetAsync(ft => ft.Id == request.Id,
                    cancellationToken: cancellationToken);
            await financialTransactionBusinessRules.FinancialTransactionMustNotBeNull(financialTransactionEntity);

            await financialTransactionRepository.DeleteAsync(financialTransactionEntity);

            return mapper.Map<DeleteFinancialTransactionResponse>(financialTransactionEntity);
        }
    }
}