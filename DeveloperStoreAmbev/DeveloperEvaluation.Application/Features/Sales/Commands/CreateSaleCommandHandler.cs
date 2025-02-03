using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DeveloperEvaluation.Application.DTOs;
using DeveloperEvaluation.Domain.Entities;
using DeveloperEvaluation.Domain.Repositories;
using MediatR;
using DeveloperEvaluation.Domain.Events;

namespace DeveloperEvaluation.Application.Features.Sales.Commands
{
    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, Guid>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CreateSaleCommandHandler(ISaleRepository saleRepository, IMapper mapper, IMediator mediator)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<Guid> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            // Mapeia os itens da venda
            var saleItems = _mapper.Map<List<SaleItem>>(request.Items);
            var sale = new Sale(request.CustomerId, saleItems);

            // Persiste a venda no banco de dados
            await _saleRepository.AddAsync(sale);

            // 🚀 Dispara o evento de criação da venda
            await _mediator.Publish(new SaleCreatedEvent(sale.Id, sale.CustomerId), cancellationToken);

            return sale.Id;
        }
    }
}
