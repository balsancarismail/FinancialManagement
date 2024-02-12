using Application.Features.InvestmentPortfolio.Commands.Create;
using Application.Features.InvestmentPortfolio.Commands.Delete;
using Application.Features.InvestmentPortfolio.Commands.Update;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvestmentPortfolioController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> CreateInvestmentPortfolio(
        [FromBody] CreateInvestmentPortfolioCommand createInvestmentPortfolioCommand)
    {
        var result = await Mediator.Send(createInvestmentPortfolioCommand);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateInvestmentPortfolio(
        [FromBody] UpdateInvestmentPortfolioCommand updateInvestmentPortfolioCommand,
        int id)
    {
        updateInvestmentPortfolioCommand.Id = id;
        var result = await Mediator.Send(updateInvestmentPortfolioCommand);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInvestmentPortfolio(int id)
    {
        var result = await Mediator.Send(new DeleteInvestmentPortfolioCommand { Id = id });
        return Ok(result);
    }
}