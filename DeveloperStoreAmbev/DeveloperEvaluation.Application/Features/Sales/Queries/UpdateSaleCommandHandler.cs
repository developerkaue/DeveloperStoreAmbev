using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DeveloperEvaluation.Domain.Repositories;
using MediatR;
using DeveloperEvaluation.Domain.Entities;

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
            if (sale == null) return false;

            var updatedItems = _mapper.Map<List<SaleItem>>(request.Items);

            // ✅ Agora existe o método UpdateItems
            sale.UpdateItems(updatedItems);

            await _saleRepository.UpdateAsync(sale);
            return true;
        }
    }
}
