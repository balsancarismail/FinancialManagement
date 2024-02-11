using Application.Features.Users.Commands.Create;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserControllers : BaseController
{
    [HttpPost("register")]
    public async Task<ActionResult<CreatedUserResponse>> Register([FromBody] CreateUserCommand command)
    {
        return await Mediator.Send(command);
    }
}