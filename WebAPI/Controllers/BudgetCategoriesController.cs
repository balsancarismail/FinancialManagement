using Application.Features.BudgetCategory.Commands.Create;
using Application.Features.BudgetCategory.Commands.Delete;
using Application.Features.BudgetCategory.Commands.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetCategoriesController : BaseController
    {

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateBudgetCategoryCommand createBudgetCategoryCommand)
        {
            var result = await Mediator.Send(createBudgetCategoryCommand);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateBudgetCategoryCommand updateBudgetCategoryCommand, int id)
        {
            updateBudgetCategoryCommand.Id = id;
            var result = await Mediator.Send(updateBudgetCategoryCommand);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteBudgetCategoryCommand = new DeleteBudgetCategoryCommand { Id = id };
            var result = await Mediator.Send(deleteBudgetCategoryCommand);
            return Ok(result);
        }

    }
}
