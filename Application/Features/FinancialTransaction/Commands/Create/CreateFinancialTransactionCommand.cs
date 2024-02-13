using System.Text.Json.Serialization;
using Application.Features.FinancialTransaction.Rules;
using Application.Services.CategoryService;
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

namespace Application.Features.FinancialTransaction.Commands.Create;

public class CreateFinancialTransactionCommand : IRequest<CreateFinancialTransactionResponse>, ICacheRemoverRequest,
    ISecuredRequest,
    ITransactionalRequest, ILoggableRequest
{
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public int CategoryId { get; set; }
    public string Description { get; set; }

    [JsonIgnore] public string CacheKey => "";

    [JsonIgnore] public bool BypassCache { get; }

    [JsonIgnore] public string CacheGroupKey => "GetFinancialTransaction";

    [JsonIgnore] public string[] Roles => new[] { USER };

    public class
        CreateFinancialTransactionCommandHandler : IRequestHandler<CreateFinancialTransactionCommand,
        CreateFinancialTransactionResponse>
    {
        private readonly ICategoryService _categoryService;
        private readonly FinancialTransactionBusinessRules _financialTransactionBusinessRules;
        private readonly IFinancialTransactionRepository _financialTransactionRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly string emailSchema = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";

        public CreateFinancialTransactionCommandHandler(IMapper mapper,
            IFinancialTransactionRepository financialTransactionRepository,
            IHttpContextAccessor httpContextAccessor,
            FinancialTransactionBusinessRules financialTransactionBusinessRules,
            UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _financialTransactionRepository = financialTransactionRepository;
            _httpContextAccessor = httpContextAccessor;
            _financialTransactionBusinessRules = financialTransactionBusinessRules;
            _userManager = userManager;
        }

        public async Task<CreateFinancialTransactionResponse> Handle(CreateFinancialTransactionCommand request,
            CancellationToken cancellationToken)
        {
            var financialTransaction = _mapper.Map<Domain.Entities.FinancialTransaction>(request);
            var userMail = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(c => c.Type == emailSchema)!
                .Value;
            var user = await _userManager.FindByEmailAsync(userMail);

            var category = await _categoryService.GetCategoryByIdAsync(request.CategoryId, cancellationToken);
            await _financialTransactionBusinessRules.CategoryMustNotBeNull(category);

            financialTransaction.AppUser = user;
            financialTransaction.Category = category;

            await _financialTransactionRepository.AddAsync(financialTransaction);

            return _mapper.Map<CreateFinancialTransactionResponse>(financialTransaction);
        }
    }
}