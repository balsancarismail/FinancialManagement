using System;
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

namespace Application.Features.Investment.Commands.Create;

public class CreateInvestmentCommand : IRequest<CreateInvestmentResponse>, ICacheRemoverRequest,
    ISecuredRequest, ITransactionalRequest, ILoggableRequest
{
    [JsonIgnore] public decimal RandomProfitOrLoss { get; set; }

    public int PortfolioId { get; set; }

    public InvestmentType InvestmentType { get; set; } // e.g., Stock, Bond
    public decimal Amount { get; set; }
    public DateTime PurchaseDate { get; set; }

    [JsonIgnore] public string CacheKey => "";

    [JsonIgnore] public bool BypassCache { get; }

    [JsonIgnore] public string CacheGroupKey => "GetInvsetment";

    [JsonIgnore] public string[] Roles => new[] { USER };

    public class
        CreateInvestmentCommandHandler(
            IInvestmentRepository investmentRepository,
            IInvestmentPortfolioService investmentPortfolioService,
            InvestmentBusinessRules investmentBusinessRules,
            IMapper mapper,
            Random random)
        : IRequestHandler<CreateInvestmentCommand, CreateInvestmentResponse>
    {
        public async Task<CreateInvestmentResponse> Handle(CreateInvestmentCommand request,
            CancellationToken cancellationToken)
        {
            var investmentPortfolio =
                await investmentPortfolioService.GetInvestmentPortfolioByIdAsync(request.PortfolioId,
                    cancellationToken);
            investmentBusinessRules.InvestmentPortfolioMustBeExists(investmentPortfolio);

            var investment = mapper.Map<Domain.Entities.Investment>(request);

            decimal changePercentage = (decimal)(random.NextDouble() * 0.2 - 0.1);
            investment.RandomProfitOrLoss = investment.Amount + changePercentage * request.Amount;
            investment.Portfolio = investmentPortfolio;

            await investmentRepository.AddAsync(investment);

            return mapper.Map<CreateInvestmentResponse>(investment);
        }
    }
}