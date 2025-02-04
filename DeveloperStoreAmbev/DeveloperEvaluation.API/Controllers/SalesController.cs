using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using DeveloperEvaluation.Application.Features.Sales.Commands;
using DeveloperEvaluation.Application.Features.Sales.Queries;

namespace DeveloperEvaluation.API.Controllers
{
    [Route("api/sales")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SalesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Criar uma nova venda
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleCommand command)
        {
            var saleId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = saleId }, saleId);
        }

        /// <summary>
        /// Buscar todas as vendas
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sales = await _mediator.Send(new GetAllSalesQuery());
            return Ok(sales);
        }

        /// <summary>
        /// Buscar uma venda pelo ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var sale = await _mediator.Send(new GetSaleByIdQuery(id));
            if (sale == null) return NotFound("Venda não encontrada.");
            return Ok(sale);
        }

        /// <summary>
        /// Atualizar uma venda (somente itens)
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSale(Guid id, [FromBody] UpdateSaleCommand command)
        {
            if (id != command.SaleId) return BadRequest("ID da venda não corresponde ao fornecido na URL.");
            var result = await _mediator.Send(command);
            if (!result) return NotFound("Venda não encontrada.");
            return NoContent();
        }

        /// <summary>
        /// Cancelar uma venda
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelSale(Guid id)
        {
            var result = await _mediator.Send(new CancelSaleCommand(id));
            if (!result) return NotFound("Venda não encontrada ou já cancelada.");
            return NoContent();
        }
    }
}
