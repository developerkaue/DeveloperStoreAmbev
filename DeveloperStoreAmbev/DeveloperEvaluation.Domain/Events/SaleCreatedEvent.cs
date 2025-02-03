using MediatR;
using System;

namespace DeveloperEvaluation.Domain.Events
{
    public class SaleCreatedEvent : INotification
    {
        public Guid SaleId { get; }
        public Guid CustomerId { get; }

        public SaleCreatedEvent(Guid saleId, Guid customerId)
        {
            SaleId = saleId;
            CustomerId = customerId;
        }
    }
}
