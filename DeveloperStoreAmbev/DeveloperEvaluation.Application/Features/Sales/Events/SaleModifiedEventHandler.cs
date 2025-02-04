using System;
using System.Threading;
using System.Threading.Tasks;
using DeveloperEvaluation.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DeveloperEvaluation.Application.Features.Sales.Events
{
    public class SaleModifiedEventHandler : INotificationHandler<SaleModifiedEvent>
    {
        private readonly ILogger<SaleModifiedEventHandler> _logger;

        public SaleModifiedEventHandler(ILogger<SaleModifiedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(SaleModifiedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"🛠️ Venda modificada! ID: {notification.SaleId} | Modificado em: {notification.ModifiedAt}");
            return Task.CompletedTask;
        }
    }
}
