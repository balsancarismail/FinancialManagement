using System.Text.Json.Serialization;
using Application.Features.Auth.Constants;
using Application.Services.RefreshToken;
using Core.Application.Pipelines.Logging;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Auth.Commands.CreateAuthenticationToken;

public class CreateAuthenticationTokenCommand(string email, string password, string ipAddress) : IRequest<CreatedAuthenticationTokenResponse>, ILoggableRequest
{
    
    public string Email { get; set; } = email;
    public string Password { get; set; } = password;
    [JsonIgnore]
    public string? IpAddress { get; set; } = ipAddress;

    public class
        CreateAuthenticationTokenCommandHandler(UserManager<AppUser> userManager, ITokenHelper tokenHelper, IRefreshTokenService refreshTokenService)
        : IRequestHandler<CreateAuthenticationTokenCommand, CreatedAuthenticationTokenResponse>
    {
        private readonly IRefreshTokenService refreshTokenService = refreshTokenService;

        public async Task<CreatedAuthenticationTokenResponse> Handle(CreateAuthenticationTokenCommand request,
            CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new NullReferenceException();
            }
            
            // if user is not found, throw an exception
            var user = await userManager.FindByEmailAsync(request.Email.Trim().Normalize());

            if (user == null)
            {
                throw new AuthorizationException(AuthMessages.UserDontExists);
            }

            var isPasswordValid = await userManager.CheckPasswordAsync(user, request.Password);

            // if password is not valid, throw an exception
            if (!isPasswordValid)
            {
                throw new AuthorizationException(AuthMessages.PasswordDontMatch);
            }

            // get user roles
            var userRoles = await userManager.GetRolesAsync(user);

            // create token and refresh token
            var token = tokenHelper.CreateToken(user, [.. userRoles]);
            var refreshToken = tokenHelper.CreateRefreshToken(user,request.IpAddress);

            // add refresh token to database
            await refreshTokenService.AddRefreshTokenAsync(refreshToken.Token, user.Id, refreshToken.Expiration,
                request.IpAddress);

            // return token and refresh token
            return new CreatedAuthenticationTokenResponse()
            {
                AccessToken = token.Token,
                RefreshToken = refreshToken.Token,
                Expires = token.Expiration
            };
        }
    }

}