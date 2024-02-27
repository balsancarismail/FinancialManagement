using System.Linq.Expressions;
using Application.Features.FinancialTransaction.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Requests;
using Core.Application.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Features.Auth.Constants.ConstantRoles;

namespace Application.Features.FinancialTransaction.Queries.GetListByUserOrCategory;

public class GetFinancialTransactionByUserOrCategoryListQuery :
    IRequest<GetListResponse<GetListFinancialTransactionByUserOrCategoryListItemDto>>, ILoggableRequest,
    ICachableRequest,
    ISecuredRequest
{
    public PageRequest PageRequest { get; set; }
    public int? AppUserId { get; set; }
    public int? CategoryId { get; set; }
    public string CacheKey => $"GetFinancialTransactionList({PageRequest.PageIndex},{PageRequest.PageSize})";
    public bool BypassCache { get; }
    public string CacheGroupKey => "GetFinancialTransaction";
    public TimeSpan? SlidingExpiration { get; init; }
    public string[] Roles => new[] { ACCOUNTANT };


    public class GetFinancialTransactionByUserOrCategoryListQueryHandler(
        IFinancialTransactionRepository financialTransactionRepository,
        IMapper mapper,
        IFinancialTransactionBusinessRules businessRules)
        : IRequestHandler<
            GetFinancialTransactionByUserOrCategoryListQuery,
            GetListResponse<GetListFinancialTransactionByUserOrCategoryListItemDto>>
    {
        public async Task<GetListResponse<GetListFinancialTransactionByUserOrCategoryListItemDto>> Handle(
            GetFinancialTransactionByUserOrCategoryListQuery request, CancellationToken cancellationToken)
        {
            await businessRules.UserIdOrCategoryIdMustBeExists(request.AppUserId, request.CategoryId);
            Expression<Func<Domain.Entities.FinancialTransaction, bool>> predicate;

            if (request.AppUserId.HasValue && request.AppUserId != 0 && request.CategoryId.HasValue &&
                request.CategoryId != 0)
                predicate = x => x.AppUserId == request.AppUserId && x.CategoryId == request.CategoryId;
            else if (request.AppUserId != 0) predicate = x => x.AppUserId == request.AppUserId;
            else predicate = x => x.CategoryId == request.CategoryId;

            var result = await financialTransactionRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                predicate: predicate,
                cancellationToken: cancellationToken,
                include: x => x.Include(y => y.Category),
                enableTracking: false);

            var response = mapper.Map<GetListResponse<GetListFinancialTransactionByUserOrCategoryListItemDto>>(result);

            return response;
        }
    }
}