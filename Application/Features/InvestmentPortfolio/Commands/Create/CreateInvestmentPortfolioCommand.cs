using System.Text.Json.Serialization;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Security.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using static Application.Features.Auth.Constants.ConstantRoles;

namespace Application.Features.InvestmentPortfolio.Commands.Create;

public class CreateInvestmentPortfolioCommand : IRequest<CreateInvestmentPortfolioResponse>, ICacheRemoverRequest,
    ISecuredRequest, ITransactionalRequest, ILoggableRequest
{
    [JsonIgnore] public int AppUserId { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }

    [JsonIgnore] public string CacheKey => "";

    [JsonIgnore] public bool BypassCache { get; }

    [JsonIgnore] public string CacheGroupKey => "GetInvsetmentPortfolio";

    [JsonIgnore] public string[] Roles => new[] { USER };

    public class
        CreateInvestmentPortfolioCommandHandler(
            IMapper mapper,
            IInvestmentPortfolioRepository investmentPortfolioRepository,
            UserManager<AppUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        : IRequestHandler<CreateInvestmentPortfolioCommand,
            CreateInvestmentPortfolioResponse>
    {
        private readonly string emailSchema = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";

        public async Task<CreateInvestmentPortfolioResponse> Handle(CreateInvestmentPortfolioCommand request,
            CancellationToken cancellationToken)
        {
            var investmentPortfolio = mapper.Map<Domain.Entities.InvestmentPortfolio>(request);
            var userMail = httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(c => c.Type == emailSchema)!
                .Value;

            var user = await userManager.FindByEmailAsync(userMail);

            investmentPortfolio.AppUser = user;

            await investmentPortfolioRepository.AddAsync(investmentPortfolio);

            return mapper.Map<CreateInvestmentPortfolioResponse>(investmentPortfolio);
        }
    }
}