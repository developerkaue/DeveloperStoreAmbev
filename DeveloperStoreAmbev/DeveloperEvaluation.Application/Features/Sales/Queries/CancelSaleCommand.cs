using System;
using MediatR;

namespace DeveloperEvaluation.Application.Features.Sales.Commands
{
    public class CancelSaleCommand : IRequest<bool>
    {
        public Guid SaleId { get; }

        public CancelSaleCommand(Guid saleId)
        {
            SaleId = saleId;
        }
    }
}
