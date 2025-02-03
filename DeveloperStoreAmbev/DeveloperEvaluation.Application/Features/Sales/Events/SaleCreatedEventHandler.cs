using System;
using System.Threading;
using System.Threading.Tasks;
using DeveloperEvaluation.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DeveloperEvaluation.Application.Features.Sales.Events
{
    public class SaleCreatedEventHandler : INotificationHandler<SaleCreatedEvent>
    {
        private readonly ILogger<SaleCreatedEventHandler> _logger;

        public SaleCreatedEventHandler(ILogger<SaleCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(SaleCreatedEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"⚡ [DEBUG] Evento disparado: Nova venda criada - ID: {notification.SaleId}");
            _logger.LogInformation($"🚀 Nova venda registrada! ID: {notification.SaleId} | Cliente: {notification.CustomerId}");

            return Task.CompletedTask;
        }
    }
}
