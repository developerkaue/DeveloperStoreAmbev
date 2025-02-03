using System;
using System.Threading;
using System.Threading.Tasks;
using DeveloperEvaluation.Domain.Events;
using DeveloperEvaluation.Application.Features.Sales.Events;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace DeveloperEvaluation.UnitTests
{
    public class SaleCreatedEventHandlerTests
    {
        [Fact]
        public async Task SaleCreatedEventHandler_Should_Log_SaleCreated()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<SaleCreatedEventHandler>>();
            var handler = new SaleCreatedEventHandler(loggerMock.Object);
            var saleCreatedEvent = new SaleCreatedEvent(Guid.NewGuid(), Guid.NewGuid());

            // Act
            await handler.Handle(saleCreatedEvent, CancellationToken.None);

            // Assert
            loggerMock.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == LogLevel.Information),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Nova venda registrada!")),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)
                ),
                Times.Once
            );
        }
    }
}
