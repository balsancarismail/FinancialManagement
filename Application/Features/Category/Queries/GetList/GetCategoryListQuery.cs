using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Requests;
using Core.Application.Responses;
using MediatR;
using static Application.Features.Auth.Constants.ConstantRoles;

namespace Application.Features.Category.Queries.GetList;

public class GetCategoryListQuery : IRequest<GetListResponse<GetListCategoryListItemDto>>, ILoggableRequest, ISecuredRequest,
    ICachableRequest
{
    public PageRequest PageRequest { get; set; }
    public string CacheKey => $"GetCategoryList({PageRequest.PageIndex},{PageRequest.PageSize})";
    public bool BypassCache { get; }
    public string CacheGroupKey => "GetCategory";
    public TimeSpan? SlidingExpiration { get; init; }
    public string[] Roles => new[] { ACCOUNTANT, USER };

    public class
        GetCategoryListQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        : IRequestHandler<GetCategoryListQuery, GetListResponse<GetListCategoryListItemDto>>
    {
        public async Task<GetListResponse<GetListCategoryListItemDto>> Handle(GetCategoryListQuery request,
            CancellationToken cancellationToken)
        {
            var result = await categoryRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                enableTracking: false);

            var categoryList = mapper.Map<GetListResponse<GetListCategoryListItemDto>>(result);

            return categoryList;
        }
    }
}