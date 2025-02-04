using System;
using System.Collections.Generic;
using DeveloperEvaluation.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace DeveloperEvaluation.UnitTests
{
    public class SaleTests
    {
        [Fact]
        public void Sale_Should_Apply_10_Percent_Discount_When_Buying_4_Or_More_Items()
        {
            // Arrange
            var items = new List<SaleItem>
            {
                new SaleItem(Guid.NewGuid(), 5, 100) // 5 unidades com preço unitário de 100
            };

            // Act
            var sale = new Sale(Guid.NewGuid(), items);

            // Assert
            sale.TotalAmount.Should().Be(450); // 500 - 10% de desconto (50)
        }

        [Fact]
        public void Sale_Should_Apply_20_Percent_Discount_When_Buying_10_Or_More_Items()
        {
            // Arrange
            var items = new List<SaleItem>
            {
                new SaleItem(Guid.NewGuid(), 10, 50) // 10 unidades com preço unitário de 50
            };

            // Act
            var sale = new Sale(Guid.NewGuid(), items);

            // Assert
            sale.TotalAmount.Should().Be(400); // 500 - 20% de desconto (100)
        }

        [Fact]
        public void Sale_Should_Not_Apply_Discount_When_Buying_Less_Than_4_Items()
        {
            // Arrange
            var items = new List<SaleItem>
            {
                new SaleItem(Guid.NewGuid(), 3, 200) // Apenas 3 unidades
            };

            // Act
            var sale = new Sale(Guid.NewGuid(), items);

            // Assert
            sale.TotalAmount.Should().Be(600); // Sem desconto
        }

        [Fact]
        public void Sale_Should_Not_Allow_More_Than_20_Items()
        {
            // Arrange & Act
            Action act = () => new SaleItem(Guid.NewGuid(), 21, 100);

            // Assert
            act.Should().Throw<InvalidOperationException>().WithMessage("Não é permitido vender mais de 20 unidades do mesmo produto.");
        }

        [Fact]
        public void Sale_Should_Be_Cancelled_When_CancelSale_Is_Called()
        {
            // Arrange
            var sale = new Sale(Guid.NewGuid(), new List<SaleItem>());

            // Act
            sale.Cancel();

            // Assert
            sale.IsCancelled.Should().BeTrue();
        }
    }
}
