using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DeveloperEvaluation.Application.Features.Sales.Commands;
using DeveloperEvaluation.Domain.Entities;
using DeveloperEvaluation.Domain.Repositories;

namespace DeveloperEvaluation.API.Controllers
{
    [Route("api/sales")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ISaleRepository _saleRepository;

        public SalesController(IMediator mediator, ISaleRepository saleRepository)
        {
            _mediator = mediator;
            _saleRepository = saleRepository;
        }

        /// <summary>
        /// Criar uma nova venda
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleCommand command)
        {
            if (command == null || command.Items == null || command.Items.Count == 0)
                return BadRequest("A venda deve conter pelo menos um item.");

            var saleId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetSaleById), new { id = saleId }, saleId);
        }

        /// <summary>
        /// Buscar todas as vendas
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllSales()
        {
            var sales = await _saleRepository.GetAllAsync();
            return Ok(sales);
        }

        /// <summary>
        /// Buscar uma venda pelo ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSaleById(Guid id)
        {
            var sale = await _saleRepository.GetByIdAsync(id);
            if (sale == null)
                return NotFound("Venda não encontrada.");

            return Ok(sale);
        }

        /// <summary>
        /// Cancelar uma venda
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelSale(Guid id)
        {
            var sale = await _saleRepository.GetByIdAsync(id);
            if (sale == null)
                return NotFound("Venda não encontrada.");

            sale.CancelSale();
            await _saleRepository.UpdateAsync(sale);
            return NoContent();
        }
    }
}
