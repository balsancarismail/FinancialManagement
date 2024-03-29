﻿using Application.Features.Category.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using MediatR;
using static Application.Features.Auth.Constants.ConstantRoles;

namespace Application.Features.Category.Queries.GetById;

public class GetCategoryByIdQuery : IRequest<GetCategoryByIdResponse>, ILoggableRequest, ICachableRequest, ISecuredRequest
{
    public int Id { get; set; }
    public string CacheKey => $"GetCategoryById({Id})";
    public bool BypassCache { get; }
    public string CacheGroupKey => "GetCategory";
    public TimeSpan? SlidingExpiration { get; init; }
    public string[] Roles => new[] { ACCOUNTANT, USER };

    public class GetCategoryByIdQueryHandler(
        ICategoryRepository categoryRepository,
        IMapper mapper,
        ICategoryBusinessRules categoryBusinessRules)
        : IRequestHandler<GetCategoryByIdQuery, GetCategoryByIdResponse>
    {
        public async Task<GetCategoryByIdResponse> Handle(GetCategoryByIdQuery request,
            CancellationToken cancellationToken)
        {
            var category = await categoryRepository.GetAsync(b => b.Id == request.Id, enableTracking: false,
                cancellationToken: cancellationToken);
            await categoryBusinessRules.CategoryMustNotBeNull(category);

            return mapper.Map<GetCategoryByIdResponse>(category);
        }
    }
}