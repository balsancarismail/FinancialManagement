using Application.Features.Auth.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Entities;
using Core.Security.JWT;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.RefreshToken;

public class RefreshTokenManager(IRefreshTokenRepository refreshTokenRepository, ITokenHelper tokenHelper, UserManager<AppUser> userManager) : IRefreshTokenService
{
    public async Task AddRefreshTokenAsync(string token, int userId, DateTime exp, string remoteIpAddress)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user is null)
            throw new AuthorizationException(AuthMessages.UserDontExists);

        var refreshToken = new Core.Security.Entities.RefreshToken
        {
            Token = token,
            UserId = userId,
            Expiration = exp,
            AppUser = user
            
        };
        await refreshTokenRepository.AddAsync(refreshToken);
    }
}