using System.Text.Json.Serialization;
using Application.Features.Budget.Rules;
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

namespace Application.Features.Budget.Commands.Create;

public class CreateBudgetCommand : IRequest<CreateBudgetResponse>, ICacheRemoverRequest, ISecuredRequest,
    ITransactionalRequest, ILoggableRequest
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    [JsonIgnore] public string CacheKey => "";

    [JsonIgnore] public bool BypassCache { get; }

    [JsonIgnore] public string CacheGroupKey => "GetBudget";

    [JsonIgnore] public string[] Roles => new[] { USER };


    public class CreateBudgetCommandHandler : IRequestHandler<CreateBudgetCommand, CreateBudgetResponse>
    {
        private readonly IBudgetRepository _budgetRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly string emailSchema = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";

        public CreateBudgetCommandHandler(IMapper mapper, IBudgetRepository budgetRepository,
            IHttpContextAccessor httpContextAccessor, BudgetBusinessRules budgetBusinessRules,
            UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _budgetRepository = budgetRepository;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<CreateBudgetResponse> Handle(CreateBudgetCommand request, CancellationToken cancellationToken)
        {
            var budget = _mapper.Map<Domain.Entities.Budget>(request);
            var userMail = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(c => c.Type == emailSchema)!
                .Value;

            var user = await _userManager.FindByEmailAsync(userMail);

            budget.AppUser = user;

            await _budgetRepository.AddAsync(budget);

            return _mapper.Map<CreateBudgetResponse>(budget);
        }
    }
}