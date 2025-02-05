using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DeveloperEvaluation.Application.DTOs;
using DeveloperEvaluation.Domain.Entities;
using DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace DeveloperEvaluation.Application.Features.Sales.Queries
{
    public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, bool>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public UpdateSaleCommandHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.SaleId);

            if (sale == null)
                throw new KeyNotFoundException("Venda não encontrada.");

            if (sale.IsCancelled)
                throw new InvalidOperationException("Não é possível modificar uma venda cancelada.");

            // Validar os itens antes de mapear
            foreach (var item in request.Items)
            {
                if (item.Quantity > 20)
                    throw new InvalidOperationException("Não é permitido vender mais de 20 unidades do mesmo produto.");
            }

            // Garantir que os itens sejam recriados corretamente
            var updatedItems = request.Items.Select(i => new SaleItem(i.ProductId, i.Quantity, i.UnitPrice)).ToList();

            sale.UpdateItems(updatedItems);

            await _saleRepository.UpdateAsync(sale);
            return true;
        }

    }
}
