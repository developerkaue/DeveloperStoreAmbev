using System;
using System.Collections.Generic;
using DeveloperEvaluation.Application.DTOs;
using MediatR;

namespace DeveloperEvaluation.Application.Features.Sales.Queries
{
    public class UpdateSaleCommand : IRequest<bool>
    {
        public Guid SaleId { get; set; }
        public List<SaleItemDto> Items { get; set; } = new(); 

        public UpdateSaleCommand()
        {
            Items = new List<SaleItemDto>();
        }
    }
}
