using Application.Features.Auth.Commands.CreateAuthenticationToken;
using Application.Features.Auth.Queries.GetAuthenticationTokenByRefreshToken;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : BaseController
{
    [HttpPost("login")]
    public async Task<ActionResult<CreatedAuthenticationTokenResponse>> Login(
        [FromBody] CreateAuthenticationTokenCommand command)
    {
        Request.Headers.TryGetValue("X-Forwarded-For", out var ipAddress);
        command.IpAddress = ipAddress;
        return await Mediator.Send(command);
    }

    [HttpGet("refresh-token")]
    public async Task<ActionResult<GetAuthenticationTokenByRefreshTokenResponse>> RefreshToken(
        [FromQuery] GetAuthenticationTokenByRefreshTokenQuery command)
    {
        return await Mediator.Send(command);
    }
}