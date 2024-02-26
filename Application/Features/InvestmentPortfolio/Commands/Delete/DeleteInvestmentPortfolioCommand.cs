using System.Text.Json.Serialization;
using Application.Features.InvestmentPortfolio.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Auth.Constants.ConstantRoles;

namespace Application.Features.InvestmentPortfolio.Commands.Delete;

public class DeleteInvestmentPortfolioCommand : IRequest<DeleteInvestmentPortfolioResponse>, ICacheRemoverRequest,
    ISecuredRequest, ITransactionalRequest, ILoggableRequest
{
    [JsonIgnore] public int Id { get; set; }

    [JsonIgnore] public string CacheKey => "";

    [JsonIgnore] public bool BypassCache { get; }

    [JsonIgnore] public string CacheGroupKey => "GetInvsetmentPortfolio";

    [JsonIgnore] public string[] Roles => new[] { USER, FINANCIALANALYST };

    public class
        DeleteInvestmentPortfolioCommandHandler(
            IMapper mapper,
            IInvestmentPortfolioRepository investmentPortfolioRepository,
            InvestmentPortfolioBusinessRules investmentPortfolioBusinessRules)
        : IRequestHandler<DeleteInvestmentPortfolioCommand,
            DeleteInvestmentPortfolioResponse>
    {
        public async Task<DeleteInvestmentPortfolioResponse> Handle(DeleteInvestmentPortfolioCommand request,
            CancellationToken cancellationToken)
        {
            var investmentPortfolioEntity =
                await investmentPortfolioRepository.GetAsync(ip => ip.Id == request.Id,
                    cancellationToken: cancellationToken);
            await investmentPortfolioBusinessRules.InvestmentPortfolioMustNotBeNull(investmentPortfolioEntity);
            await investmentPortfolioRepository.DeleteAsync(investmentPortfolioEntity);

            return mapper.Map<DeleteInvestmentPortfolioResponse>(investmentPortfolioEntity);
        }
    }
}