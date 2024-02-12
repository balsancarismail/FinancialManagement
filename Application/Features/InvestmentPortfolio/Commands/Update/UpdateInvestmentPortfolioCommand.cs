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

namespace Application.Features.InvestmentPortfolio.Commands.Update;

public class UpdateInvestmentPortfolioCommand : IRequest<UpdateInvestmentPortfolioResponse>, ICacheRemoverRequest,
    ISecuredRequest, ITransactionalRequest, ILoggableRequest
{
    [JsonIgnore] public int Id { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }

    [JsonIgnore] public string CacheKey => "";

    [JsonIgnore] public bool BypassCache { get; }

    [JsonIgnore] public string CacheGroupKey => "GetInvsetmentPortfolio";

    [JsonIgnore] public string[] Roles => new[] { USER };

    public class
        UpdateInvestmentPortfolioCommandHandler(
            IMapper mapper,
            IInvestmentPortfolioRepository investmentPortfolioRepository,
            InvestmentPortfolioBusinessRules investmentPortfolioBusinessRules
        )
        : IRequestHandler<UpdateInvestmentPortfolioCommand,
            UpdateInvestmentPortfolioResponse>
    {
        public async Task<UpdateInvestmentPortfolioResponse> Handle(UpdateInvestmentPortfolioCommand request,
            CancellationToken cancellationToken)
        {
            var investmentPortfolio = mapper.Map<Domain.Entities.InvestmentPortfolio>(request);
            var investmentPortfolioEntity =
                await investmentPortfolioRepository.GetAsync(ip => ip.Id == request.Id,
                    cancellationToken: cancellationToken);
            await investmentPortfolioBusinessRules.InvestmentPortfolioMustNotBeNull(investmentPortfolioEntity);

            mapper.Map(investmentPortfolio, investmentPortfolioEntity);
            await investmentPortfolioRepository.UpdateAsync(investmentPortfolioEntity);

            return mapper.Map<UpdateInvestmentPortfolioResponse>(investmentPortfolioEntity);
        }
    }
}