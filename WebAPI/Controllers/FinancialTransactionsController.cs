﻿using Application.Features.FinancialTransaction.Commands.Create;
using Application.Features.FinancialTransaction.Commands.Delete;
using Application.Features.FinancialTransaction.Commands.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialTransactionsController : BaseController
    {

        [HttpPost]
        public async Task<ActionResult<CreateFinancialTransactionResponse>> Create(CreateFinancialTransactionCommand createFinancialTransactionCommand)
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

    }
}