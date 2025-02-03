using System;
using MediatR;

namespace DeveloperEvaluation.Domain.Events
{
    public class ItemCancelledEvent : INotification
    {
        public Guid SaleId { get; }
        public Guid ProductId { get; }
        public DateTime CancelledAt { get; }

        public ItemCancelledEvent(Guid saleId, Guid productId)
        {
            SaleId = saleId;
            ProductId = productId;
            CancelledAt = DateTime.UtcNow;
        }
    }
}
