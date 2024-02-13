using System.Text.Json.Serialization;
using Application.Features.Category.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Auth.Constants.ConstantRoles;

namespace Application.Features.Category.Commands.Delete;

public class DeleteCategoryCommand : IRequest<DeleteCategoryResponse>, ICacheRemoverRequest, ISecuredRequest,
    ITransactionalRequest, ILoggableRequest
{
    [JsonIgnore] public int Id { get; set; }

    [JsonIgnore] public string CacheKey => "";

    [JsonIgnore] public bool BypassCache { get; }

    [JsonIgnore] public string CacheGroupKey => "GetCategory";

    [JsonIgnore] public string[] Roles => new[] { USER };

    public class DeleteCategoryCommandHandler(
        ICategoryRepository categoryRepository,
        IMapper mapper,
        CategoryBusinessRules categoryBusiness)
        : IRequestHandler<DeleteCategoryCommand, DeleteCategoryResponse>
    {
        public async Task<DeleteCategoryResponse> Handle(DeleteCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var categoryEntity = await categoryRepository.GetAsync(c => c.Id == request.Id,
                cancellationToken: cancellationToken);

            await categoryRepository.DeleteAsync(categoryEntity);

            return mapper.Map<DeleteCategoryResponse>(categoryEntity);
        }
    }
}