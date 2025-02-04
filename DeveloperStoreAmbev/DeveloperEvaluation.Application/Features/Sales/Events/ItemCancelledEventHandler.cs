using System;
using System.Threading;
using System.Threading.Tasks;
using DeveloperEvaluation.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DeveloperEvaluation.Application.Features.Sales.Events
{
    public class ItemCancelledEventHandler : INotificationHandler<ItemCancelledEvent>
    {
        private readonly ILogger<ItemCancelledEventHandler> _logger;

        public ItemCancelledEventHandler(ILogger<ItemCancelledEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(ItemCancelledEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"❌ Item cancelado! Venda ID: {notification.SaleId} | Produto ID: {notification.ProductId} | Cancelado em: {notification.CancelledAt}");
            return Task.CompletedTask;
        }
    }
}
