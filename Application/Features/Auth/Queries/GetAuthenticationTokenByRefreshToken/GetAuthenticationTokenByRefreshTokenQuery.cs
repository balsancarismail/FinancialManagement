using Application.Features.Auth.Constants;
using Application.Services.Repositories;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;
using Microsoft.AspNetCore.Identity;
using static Application.Features.Auth.Constants.ConstantRoles;

namespace Application.Features.Auth.Queries.GetAuthenticationTokenByRefreshToken;

public class GetAuthenticationTokenByRefreshTokenQuery : IRequest<GetAuthenticationTokenByRefreshTokenResponse>,
    ILoggableRequest, ISecuredRequest
{
    public string RefreshToken { get; set; }

    public string[] Roles => [USER];

    public class GetAuthenticationTokenByRefreshTokenQueryHandler
        : IRequestHandler<GetAuthenticationTokenByRefreshTokenQuery, GetAuthenticationTokenByRefreshTokenResponse>

    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly ITokenHelper _tokenHelper;
        private readonly UserManager<AppUser> _userManager;

        public GetAuthenticationTokenByRefreshTokenQueryHandler(UserManager<AppUser> userManager,
            ITokenHelper tokenHelper, IRefreshTokenRepository refreshTokenRepository)
        {
            _userManager = userManager;
            _tokenHelper = tokenHelper;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<GetAuthenticationTokenByRefreshTokenResponse> Handle(
            GetAuthenticationTokenByRefreshTokenQuery request, CancellationToken cancellationToken)
        {
            var refreshToken = await _refreshTokenRepository.GetAsync(
                x => x.Token == request.RefreshToken,
                enableTracking: false, cancellationToken: cancellationToken);

            if (refreshToken is null)
                throw new AuthorizationException(AuthMessages.RefreshDontExists);
            if (refreshToken.Expiration < DateTime.UtcNow)
                throw new AuthorizationException(AuthMessages.RefreshExpired);
            if (refreshToken.Revoked is not null && refreshToken.Revoked < DateTime.Now)
                throw new AuthorizationException(AuthMessages.RefreshRevoked);

            var user = await _userManager.FindByIdAsync(refreshToken.UserId.ToString());
            var userRoles = await _userManager.GetRolesAsync(user);

            var token = _tokenHelper.CreateToken(user, userRoles.ToList());

            return new GetAuthenticationTokenByRefreshTokenResponse
            {
                AccessToken = token.Token,
                Expires = token.Expiration,
                RefreshToken = refreshToken.Token
            };
        }
    }
}