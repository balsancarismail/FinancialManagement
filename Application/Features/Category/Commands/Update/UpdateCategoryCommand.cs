using System.Text.Json.Serialization;
using Application.Features.Category.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Domain.Enums;
using MediatR;
using static Application.Features.Auth.Constants.ConstantRoles;

namespace Application.Features.Category.Commands.Update;

public class UpdateCategoryCommand : IRequest<UpdateCategoryResponse>, ICacheRemoverRequest, ISecuredRequest,
    ITransactionalRequest, ILoggableRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public CategoryType CategoryType { get; set; }

    [JsonIgnore] public string CacheKey => "";

    [JsonIgnore] public bool BypassCache { get; }

    [JsonIgnore] public string CacheGroupKey => "GetCategory";

    [JsonIgnore] public string[] Roles => new[] { USER };

    public class UpdateCategoryCommandHandler(
        ICategoryRepository categoryRepository,
        IMapper mapper,
        CategoryBusinessRules categoryBusiness)
        : IRequestHandler<UpdateCategoryCommand, UpdateCategoryResponse>
    {
        public async Task<UpdateCategoryResponse> Handle(UpdateCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var categoryEntity = await categoryRepository.GetAsync(c => c.Id == request.Id,
                cancellationToken: cancellationToken);

            await categoryBusiness.CategoryMustNotBeNull(categoryEntity);

            var category = mapper.Map<Domain.Entities.Category>(request);

            mapper.Map(category, categoryEntity);
            await categoryRepository.UpdateAsync(categoryEntity);
            return mapper.Map<UpdateCategoryResponse>(categoryEntity);
        }
    }
}