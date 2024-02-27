using Application.Features.FinancialTransaction.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Features.Auth.Constants.ConstantRoles;

namespace Application.Features.FinancialTransaction.Queries.GetById;

public class GetFinancialTransactionByIdQuery : IRequest<GetFinancialTransactionByIdResponse>, ILoggableRequest,
    ICachableRequest,
    ISecuredRequest
{
    public int Id { get; set; }
    public string CacheKey => $"GetFinancialTransactionById({Id})";
    public bool BypassCache { get; }
    public string CacheGroupKey => "GetFinancialTransaction";
    public TimeSpan? SlidingExpiration { get; init; }
    public string[] Roles => new[] { ACCOUNTANT, USER };

    public class GetFinancialTransactionByIdQueryHandler(
        IFinancialTransactionRepository financialTransactionRepository,
        IMapper mapper,
        IFinancialTransactionBusinessRules financialTransactionBusinessRules)
        : IRequestHandler<GetFinancialTransactionByIdQuery, GetFinancialTransactionByIdResponse>
    {
        public async Task<GetFinancialTransactionByIdResponse> Handle(GetFinancialTransactionByIdQuery request,
            CancellationToken cancellationToken)
        {
            var financialTransaction = await financialTransactionRepository.GetAsync(b => b.Id == request.Id,
                enableTracking: false, cancellationToken: cancellationToken,
                include: src => src.Include(b => b.Category));
            await financialTransactionBusinessRules.FinancialTransactionMustNotBeNull(financialTransaction);

            return mapper.Map<GetFinancialTransactionByIdResponse>(financialTransaction);
        }
    }
}