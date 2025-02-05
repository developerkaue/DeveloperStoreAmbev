using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DeveloperEvaluation.Application.DTOs;
using DeveloperEvaluation.Domain.Entities;
using DeveloperEvaluation.Domain.Repositories;
using MediatR;
using DeveloperEvaluation.Application.Events;

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
            // Validar manualmente os itens antes de mapear
            foreach (var item in request.Items)
            {
                if (item.Quantity > 20)
                    throw new InvalidOperationException("Não é permitido vender mais de 20 unidades do mesmo produto.");
            }

            // Agora podemos mapear os itens corretamente
            var saleItems = _mapper.Map<List<SaleItem>>(request.Items);
            var sale = new Sale(request.CustomerId, saleItems);

            await _saleRepository.AddAsync(sale);
            await _mediator.Publish(new SaleCreatedEvent(sale));

            return sale.Id;
        }

    }
}
