using Application.Features.FinancialTransaction.Commands.Create;
using Application.Features.FinancialTransaction.Commands.Delete;
using Application.Features.FinancialTransaction.Commands.Update;
using Application.Features.FinancialTransaction.Queries.GetById;
using Application.Features.FinancialTransaction.Queries.GetListByUserOrCategory;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FinancialTransactionsController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<CreateFinancialTransactionResponse>> Create(
        CreateFinancialTransactionCommand createFinancialTransactionCommand)
    {
        var ok = await Mediator.Send(createFinancialTransactionCommand);
        return Ok(ok);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CreateFinancialTransactionResponse>> Update(
        UpdateFinancialTransactionCommand updateFinancialTransactionCommand, int id)
    {
        updateFinancialTransactionCommand.Id = id;
        var ok = await Mediator.Send(updateFinancialTransactionCommand);
        return Ok(ok);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<CreateFinancialTransactionResponse>> Delete(int id)
    {
        var deleteFinancialTransactionCommand = new DeleteFinancialTransactionCommand { Id = id };
        var ok = await Mediator.Send(deleteFinancialTransactionCommand);
        return Ok(ok);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetFinancialTransactionByIdResponse>> GetById(int id)
    {
        var getFinancialTransactionByIdQuery = new GetFinancialTransactionByIdQuery { Id = id };
        var ok = await Mediator.Send(getFinancialTransactionByIdQuery);
        return Ok(ok);
    }

    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetListFinancialTransactionByUserOrCategoryListItemDto>>>
        GetListByUserOrCategory([FromQuery] PageRequest pageRequest, [FromQuery] int? AppUserId, [FromQuery] int? categoryId)
    {
        var ok = await Mediator.Send(new GetFinancialTransactionByUserOrCategoryListQuery()
        {
            AppUserId = AppUserId,
            CategoryId = categoryId,
            PageRequest = pageRequest
        });
        return Ok(ok);
    }
}