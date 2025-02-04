using System;
using System.Threading;
using System.Threading.Tasks;
using DeveloperEvaluation.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DeveloperEvaluation.Application.Features.Sales.Events
{
    public class SaleCancelledEventHandler : INotificationHandler<SaleCancelledEvent>
    {
        private readonly ILogger<SaleCancelledEventHandler> _logger;

        public SaleCancelledEventHandler(ILogger<SaleCancelledEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(SaleCancelledEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"❌ Venda cancelada! ID: {notification.SaleId} | Cancelado em: {notification.CancelledAt}");
            return Task.CompletedTask;
        }
    }
}
