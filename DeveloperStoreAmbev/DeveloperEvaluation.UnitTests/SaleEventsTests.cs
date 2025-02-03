using System;
using System.Threading;
using System.Threading.Tasks;
using DeveloperEvaluation.Application.DTOs;
using DeveloperEvaluation.Application.Features.Sales.Commands;
using DeveloperEvaluation.Application.Features.Sales.Events;
using DeveloperEvaluation.Domain.Events;
using DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace DeveloperEvaluation.UnitTests
{
    public class SaleEventsTests
    {
        [Fact]
        public async Task CreateSaleCommand_Should_Publish_SaleCreatedEvent()
        {
            // Arrange
            var saleRepositoryMock = new Mock<ISaleRepository>();
            var mediatorMock = new Mock<IMediator>();
            var handler = new CreateSaleCommandHandler(saleRepositoryMock.Object, new Mock<AutoMapper.IMapper>().Object, mediatorMock.Object);

            var command = new CreateSaleCommand
            {
                CustomerId = Guid.NewGuid(),
                Items = new System.Collections.Generic.List<SaleItemDto>
                {
                    new SaleItemDto { ProductId = Guid.NewGuid(), Quantity = 5, UnitPrice = 100 }
                }
            };

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mediatorMock.Verify(m => m.Publish(It.IsAny<SaleCreatedEvent>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task SaleModifiedEventHandler_Should_Log_ModifiedSale()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<SaleModifiedEventHandler>>();
            var handler = new SaleModifiedEventHandler(loggerMock.Object);
            var saleModifiedEvent = new SaleModifiedEvent(Guid.NewGuid());

            // Act
            await handler.Handle(saleModifiedEvent, CancellationToken.None);

            // Assert
            loggerMock.Verify(x => x.LogInformation(It.IsAny<string>(), It.IsAny<object[]>()), Times.Once);
        }
    }
}

