using ApiEstoqueASP.Data.DTOs;
using ApiEstoqueASP.Models;
using System.Net;
using System.Net.Http.Json;

namespace ApiEstoqueASP.Integration.Test
{
    public class OrderItem_GET : IClassFixture<ApiEstoqueASPFactory>
    {
        private readonly ApiEstoqueASPFactory _app;

        public OrderItem_GET()
        {
            this._app = new ApiEstoqueASPFactory();
        }

        [Fact]
        public async Task GET_Existent_OrderItem_Returns_Ok()
        {
            // Arrange
            using HttpClient client = await this._app.GetHttpClientWithAuthenticationTokenAsync();

            OrderItem existentOrderItem = this._app.GetExistentOrderItemOrCreate();

            HttpStatusCode expectedStatus = HttpStatusCode.OK;


            // Act
            HttpResponseMessage response = await client.GetAsync($"orderItem/{existentOrderItem.Id}");
            ReadOrderItemDto orderItemResponse = await response.Content.ReadFromJsonAsync<ReadOrderItemDto>();



            // Assert
            Assert.NotNull(response);
            Assert.NotNull(orderItemResponse);
            Assert.Equal(expectedStatus, response.StatusCode);
            Assert.Equal(existentOrderItem.Quantity, orderItemResponse.Quantity);
            Assert.Equal(existentOrderItem.ProductId, orderItemResponse.ProductId);
            Assert.Equal(existentOrderItem.UserId, orderItemResponse.UserId);
        }


        [Fact]
        public async Task GET_NonExistent_OrderItem_Returns_NotFound()
        {
            // Arrange
            using HttpClient client = await this._app.GetHttpClientWithAuthenticationTokenAsync();

            HttpStatusCode expectedStatus = HttpStatusCode.NotFound;


            // Act
            HttpResponseMessage response = await client.GetAsync("orderItem/94729724");


            // Assert
            Assert.NotNull(response);
            Assert.Equal(expectedStatus, response.StatusCode);
        }
    }
}
