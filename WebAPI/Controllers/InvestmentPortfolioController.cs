using Application.Features.InvestmentPortfolio.Commands.Create;
using Application.Features.InvestmentPortfolio.Commands.Delete;
using Application.Features.InvestmentPortfolio.Commands.Update;
using Application.Features.InvestmentPortfolio.Queries.GetById;
using Application.Features.InvestmentPortfolio.Queries.GetInvestmentPortfolioBreakdown;
using Application.Features.InvestmentPortfolio.Queries.GetListInvestmentPortfolio;
using Core.Application.Requests;
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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetInvestmentPortfolioById(int id)
    {
        var result = await Mediator.Send(new GetInvestmentPortfolioByIdQuery { Id = id });
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetInvestmentPortfolioList([FromQuery] PageRequest pageRequest)
    {
        var query = new GetInvestmentPortfolioListQuery { PageRequest = pageRequest };
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("breakdown/{id}")]
    public async Task<IActionResult> GetBudgetBreakdown(int id)
    {
        var query = new GetInvestmentPortfolioBreakdownQuery { Id = id };
        var response = await Mediator.Send(query);
        return Ok(response);
    }
}