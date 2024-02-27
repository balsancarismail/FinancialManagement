using System.Text.Json.Serialization;
using Application.Features.Investment.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Auth.Constants.ConstantRoles;

namespace Application.Features.Investment.Commands.Delete;

public class DeleteInvestmentCommand : IRequest<DeleteInvestmentResponse>, ICacheRemoverRequest,
    ISecuredRequest, ITransactionalRequest, ILoggableRequest
{
    [JsonIgnore] public int Id { get; set; }

    [JsonIgnore] public string CacheKey => "";

    [JsonIgnore] public bool BypassCache { get; }

    [JsonIgnore] public string CacheGroupKey => "GetInvsetment";

    [JsonIgnore] public string[] Roles => new[] { USER, FINANCIALANALYST };

    public class
        DeleteInvestmentCommandHandler(
            IInvestmentRepository investmentRepository,
            IInvestmentBusinessRules investmentBusinessRules,
            IMapper mapper)
        : IRequestHandler<DeleteInvestmentCommand, DeleteInvestmentResponse>
    {
        public async Task<DeleteInvestmentResponse> Handle(DeleteInvestmentCommand request,
            CancellationToken cancellationToken)
        {
            var investmentEntity =
                await investmentRepository.GetAsync(i => i.Id == request.Id, cancellationToken: cancellationToken);

            investmentBusinessRules.InvestmentMustBeExists(investmentEntity);

            await investmentRepository.DeleteAsync(investmentEntity);

            return mapper.Map<DeleteInvestmentResponse>(investmentEntity);
        }
    }
}