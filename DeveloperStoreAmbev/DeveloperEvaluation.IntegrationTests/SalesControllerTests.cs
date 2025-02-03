using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DeveloperEvaluation.API;
using DeveloperEvaluation.Application.DTOs;
using DeveloperEvaluation.Application.Features.Sales.Commands;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

namespace DeveloperEvaluation.IntegrationTests
{
    public class SalesControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public SalesControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateSale_Should_Return_201Created()
        {
            // Arrange
            var saleCommand = new CreateSaleCommand
            {
                CustomerId = Guid.NewGuid(),
                Items = new List<SaleItemDto>
                {
                    new SaleItemDto { ProductId = Guid.NewGuid(), Quantity = 5, UnitPrice = 100 }
                }
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/sales", saleCommand);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }

        [Fact]
        public async Task GetSales_Should_Return_List_Of_Sales()
        {
            // Act
            var response = await _client.GetAsync("/api/sales");

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            var sales = await response.Content.ReadFromJsonAsync<List<dynamic>>();
            sales.Should().NotBeNull();
        }
    }
}
