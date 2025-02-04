using System.Threading;
using System.Threading.Tasks;
using DeveloperEvaluation.Application.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DeveloperEvaluation.Application.Handlers
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
            _logger.LogInformation($"Nova venda registrada! ID: {notification.Sale.Id}");
            return Task.CompletedTask;
        }
    }
}
