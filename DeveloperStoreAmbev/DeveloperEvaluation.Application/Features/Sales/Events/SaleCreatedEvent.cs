using DeveloperEvaluation.Domain.Entities;
using MediatR;
using System;

namespace DeveloperEvaluation.Application.Events
{
    public class SaleCreatedEvent : INotification
    {
        public Sale Sale { get; }

        public SaleCreatedEvent(Sale sale)
        {
            Sale = sale ?? throw new ArgumentNullException(nameof(sale));
        }
    }
}
