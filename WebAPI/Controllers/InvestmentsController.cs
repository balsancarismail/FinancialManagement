using Application.Features.Investment.Commands.Create;
using Application.Features.Investment.Commands.Delete;
using Application.Features.Investment.Commands.Update;
using Application.Features.Investment.Queries.GetById;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvestmentsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> CreateInvestment([FromBody] CreateInvestmentCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateInvestment(int id, [FromBody] UpdateInvestmentCommand command)
    {
        command.Id = id;
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInvestment(int id)
    {
        var command = new DeleteInvestmentCommand { Id = id };
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetInvestmentById(int id)
    {
        var query = new GetInvestmentByIdQuery { Id = id };
        var result = await Mediator.Send(query);
        return Ok(result);
    }

}