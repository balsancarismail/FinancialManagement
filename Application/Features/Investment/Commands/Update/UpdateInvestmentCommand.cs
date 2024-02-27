using System.Text.Json.Serialization;
using Application.Features.Investment.Rules;
using Application.Services.InvestmentPortfolioService;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Domain.Enums;
using MediatR;
using static Application.Features.Auth.Constants.ConstantRoles;

namespace Application.Features.Investment.Commands.Update;

public class UpdateInvestmentCommand : IRequest<UpdateInvestmentResponse>, ICacheRemoverRequest,
    ISecuredRequest, ITransactionalRequest, ILoggableRequest
{
    [JsonIgnore] public decimal RandomProfitOrLoss { get; set; }

    [JsonIgnore] public int Id { get; set; }

    public InvestmentType InvestmentType { get; set; } // e.g., Stock, Bond
    public decimal Amount { get; set; }
    public DateTime PurchaseDate { get; set; }

    [JsonIgnore] public string CacheKey => "";

    [JsonIgnore] public bool BypassCache { get; }

    [JsonIgnore] public string CacheGroupKey => "GetInvsetment";

    [JsonIgnore] public string[] Roles => new[] { USER, FINANCIALANALYST };

    public class
        UpdateInvestmentCommandHandler(
            IInvestmentRepository investmentRepository,
            IInvestmentPortfolioService investmentPortfolioService,
            IInvestmentBusinessRules investmentBusinessRules,
            IMapper mapper,
            Random random)
        : IRequestHandler<UpdateInvestmentCommand, UpdateInvestmentResponse>
    {
        public async Task<UpdateInvestmentResponse> Handle(UpdateInvestmentCommand request,
            CancellationToken cancellationToken)
        {
            var investmentEntity =
                await investmentRepository.GetAsync(i => i.Id == request.Id, cancellationToken: cancellationToken);
            var investment = mapper.Map<Domain.Entities.Investment>(request);

            if (investment.Amount != investmentEntity.Amount)
            {
                var changePercentage = (decimal)(random.NextDouble() * 0.2 - 0.1);
                investment.RandomProfitOrLoss = investment.Amount - changePercentage * request.Amount;
            }

            investmentBusinessRules.InvestmentMustBeExists(investmentEntity);

            mapper.Map(investment, investmentEntity);
            await investmentRepository.UpdateAsync(investmentEntity);

            return mapper.Map<UpdateInvestmentResponse>(investmentEntity);
        }
    }
}