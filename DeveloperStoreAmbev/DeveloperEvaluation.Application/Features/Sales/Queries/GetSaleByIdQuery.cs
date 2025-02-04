using System;
using MediatR;
using DeveloperEvaluation.Application.DTOs;

namespace DeveloperEvaluation.Application.Features.Sales.Queries
{
    public class GetSaleByIdQuery : IRequest<SaleDto>
    {
        public Guid SaleId { get; }

        public GetSaleByIdQuery(Guid saleId)
        {
            SaleId = saleId;
        }
    }
}
