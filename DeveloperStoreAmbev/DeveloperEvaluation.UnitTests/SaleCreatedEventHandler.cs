using System;
using System.Threading;
using System.Threading.Tasks;
using DeveloperEvaluation.Application.Events;
using DeveloperEvaluation.Application.Features.Sales.Events;
using DeveloperEvaluation.Application.Handlers;
using DeveloperEvaluation.Domain.Entities;
using DeveloperEvaluation.Domain.Events;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace DeveloperEvaluation.UnitTests
{
    public class SaleCreatedEventHandlerTests
    {
        private readonly Mock<ILogger<SaleCreatedEventHandler>> _loggerMock;
        private readonly SaleCreatedEventHandler _handler;

        public SaleCreatedEventHandlerTests()
        {
            _loggerMock = new Mock<ILogger<SaleCreatedEventHandler>>();
            _handler = new SaleCreatedEventHandler(_loggerMock.Object);
        }

        [Fact]
        public async Task Handle_Should_Log_Message_When_Sale_Created()
        {
            var sale = new Sale(Guid.NewGuid(), new List<SaleItem>());
            var saleCreatedEvent = new SaleCreatedEvent(sale);

            await _handler.Handle(saleCreatedEvent, CancellationToken.None);

            _loggerMock.Verify(
                x => x.Log(
                    It.IsAny<LogLevel>(),
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception?>(), // ✅ Ajustando para permitir nulo
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>() // ✅ Ajustando para Exception?
                ),
                Times.Once
            );
        }
    }
}
