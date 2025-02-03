using MediatR;
using System;

namespace DeveloperEvaluation.Domain.Events
{
    public class SaleModifiedEvent : INotification
    {
        public Guid SaleId { get; }
        public SaleModifiedEvent(Guid saleId)
        {
            SaleId = saleId;
        }
    }
}
