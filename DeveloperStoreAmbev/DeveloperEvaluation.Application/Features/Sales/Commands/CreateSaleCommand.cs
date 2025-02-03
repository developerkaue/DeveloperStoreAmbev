using DeveloperEvaluation.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;

namespace DeveloperEvaluation.Application.Features.Sales.Commands
{
    public class CreateSaleCommand : IRequest<Guid>
    {
        public Guid CustomerId { get; set; }
        public List<SaleItemDto> Items { get; set; } = new List<SaleItemDto>();
    }

}