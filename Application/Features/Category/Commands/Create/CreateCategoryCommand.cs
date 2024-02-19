using System.Text.Json.Serialization;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Auth.Constants.ConstantRoles;

namespace Application.Features.Category.Commands.Create;

public class CreateCategoryCommand : IRequest<CreateCategoryResponse>, ICacheRemoverRequest, ISecuredRequest,
    ITransactionalRequest, ILoggableRequest
{
    public string Name { get; set; }
    public int CategoryType { get; set; }

    [JsonIgnore] public string CacheKey => "";

    [JsonIgnore] public bool BypassCache { get; }

    [JsonIgnore] public string CacheGroupKey => "GetCategory";

    [JsonIgnore] public string[] Roles => new[] { USER };

    public class CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        : IRequestHandler<CreateCategoryCommand, CreateCategoryResponse>
    {
        public async Task<CreateCategoryResponse> Handle(CreateCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var category = mapper.Map<Domain.Entities.Category>(request);
            var result = await categoryRepository.AddAsync(category);
            return mapper.Map<CreateCategoryResponse>(result);
        }
    }
}