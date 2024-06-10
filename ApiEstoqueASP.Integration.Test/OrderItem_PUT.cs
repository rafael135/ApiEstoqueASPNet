using ApiEstoqueASP.Data.DTOs;
using ApiEstoqueASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ApiEstoqueASP.Integration.Test
{
    public class OrderItem_PUT : IClassFixture<ApiEstoqueASPFactory>
    {
        private readonly ApiEstoqueASPFactory _app;

        public OrderItem_PUT()
        {
            this._app = new ApiEstoqueASPFactory();
        }

        [Fact]
        public async Task PUT_Update_Existent_OrderItem_Returns_NoContent()
        {
            // Arrange
            using HttpClient client = await this._app.GetHttpClientWithAuthenticationTokenAsync();

            OrderItem existentOrderItem = this._app.GetExistentOrderItemOrCreate();

            UpdateOrderItemDto data = new UpdateOrderItemDto()
            {
                OrderId = existentOrderItem.OrderId,
                ProductId = existentOrderItem.ProductId,
                Quantity = existentOrderItem.Quantity
            };

            data.Quantity = new Random().Next(1, 5);
            HttpStatusCode expectedStatus = HttpStatusCode.NoContent;


            // Act
            HttpResponseMessage response = await client.PutAsJsonAsync<UpdateOrderItemDto>($"/orderItem/{existentOrderItem.Id}", data);



            // Assert
            Assert.NotNull(response);
            Assert.Equal(expectedStatus, response.StatusCode);
        }

        [Fact]
        public async Task PUT_Update_NonExistent_OrderItem_Returns_NotFound()
        {
            // Arrange
            using HttpClient client = await this._app.GetHttpClientWithAuthenticationTokenAsync();

            OrderItem existentOrderItem = new OrderItem()
            {
                Id = 29472421,
                OrderId = 32747,
                ProductId = new Random().Next(1, 20),
                Quantity = new Random().Next(1, 20)
            };

            UpdateOrderItemDto data = new UpdateOrderItemDto()
            {
                OrderId = existentOrderItem.OrderId,
                ProductId = existentOrderItem.ProductId,
                Quantity = existentOrderItem.Quantity
            };

            data.Quantity = new Random().Next(1, 5);
            HttpStatusCode expectedStatus = HttpStatusCode.NotFound;


            // Act
            HttpResponseMessage response = await client.PutAsJsonAsync<UpdateOrderItemDto>($"/orderItem/{existentOrderItem.Id}", data);



            // Assert
            Assert.NotNull(response);
            Assert.Equal(expectedStatus, response.StatusCode);
        }
    }
}
