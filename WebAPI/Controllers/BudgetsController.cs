using Application.Features.Budget.Commands.Create;
using Application.Features.Budget.Commands.Delete;
using Application.Features.Budget.Commands.Update;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetsController : BaseController
    {

        [HttpPost]
        public async Task<IActionResult> CreateBudget([FromBody] CreateBudgetCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBudget(int id)
        {
            var command = new DeleteBudgetCommand { Id = id };
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBudget(int id, [FromBody] UpdateBudgetCommand command)
        {
            command.Id = id;
            var response = await Mediator.Send(command);
            return Ok(response);
        }
    }

}
