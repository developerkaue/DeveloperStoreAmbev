using MediatR;
using System;

namespace DeveloperEvaluation.Domain.Events
{
    public class ItemCancelledEvent : INotification
    {
        public Guid SaleId { get; }
        public Guid ProductId { get; }

        public ItemCancelledEvent(Guid saleId, Guid productId)
        {
            SaleId = saleId;
            ProductId = productId;
        }
    }
}
