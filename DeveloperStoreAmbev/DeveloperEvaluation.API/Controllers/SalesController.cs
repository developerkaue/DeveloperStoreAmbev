using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using DeveloperEvaluation.Application.Features.Sales.Commands;
using DeveloperEvaluation.Application.Features.Sales.Queries;
using Microsoft.EntityFrameworkCore;

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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var saleId = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetById), new { id = saleId }, new { saleId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno ao criar a venda.", details = ex.Message });
            }
        }

        /// <summary>
        /// Buscar todas as vendas
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var sales = await _mediator.Send(new GetAllSalesQuery());
                return Ok(sales);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao buscar as vendas.", details = ex.Message });
            }
        }

        /// <summary>
        /// Buscar uma venda pelo ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest(new { message = "ID inválido." });

            try
            {
                var sale = await _mediator.Send(new GetSaleByIdQuery(id));
                if (sale == null)
                    return NotFound(new { message = "Venda não encontrada." });

                return Ok(sale);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao buscar a venda.", details = ex.Message });
            }
        }

        /// <summary>
        /// Atualizar uma venda (somente itens)
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSale(Guid id, [FromBody] UpdateSaleCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != command.SaleId)
                return BadRequest(new { message = "O ID da URL não corresponde ao ID da requisição." });

            try
            {
                var result = await _mediator.Send(command);

                if (!result)
                    return NotFound(new { message = "Venda não encontrada ou não pode ser atualizada." });

                return Ok(new { message = "Venda atualizada com sucesso." });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = "Erro de operação inválida.", details = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Conflict(new { message = "A venda foi modificada por outra operação.", details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno ao atualizar a venda.", details = ex.Message });
            }
        }


        /// <summary>
        /// Cancelar uma venda
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelSale(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest(new { message = "ID inválido." });

            try
            {
                var result = await _mediator.Send(new CancelSaleCommand(id));

                if (!result)
                    return NotFound(new { message = "Venda não encontrada ou já cancelada." });

                return Ok(new { message = "Venda cancelada com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao cancelar a venda.", details = ex.Message });
            }
        }

    }
}
