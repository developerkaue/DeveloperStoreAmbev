using System;
using System.Threading;
using System.Threading.Tasks;
using DeveloperEvaluation.Application.DTOs;
using DeveloperEvaluation.Application.Features.Sales.Commands;
using DeveloperEvaluation.Domain.Events;
using DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using MediatR;
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
    }
}
