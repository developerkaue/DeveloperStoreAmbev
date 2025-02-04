using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DeveloperEvaluation.Application.DTOs;
using DeveloperEvaluation.Domain.Entities;
using DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace DeveloperEvaluation.Application.Features.Sales.Commands
{
    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, Guid>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public CreateSaleCommandHandler(ISaleRepository saleRepository, IMapper mapper, IMediator @object)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var saleItems = _mapper.Map<List<SaleItem>>(request.Items);
            var sale = new Sale(request.CustomerId, saleItems);
            await _saleRepository.AddAsync(sale);
            return sale.Id;
        }
    }
}
