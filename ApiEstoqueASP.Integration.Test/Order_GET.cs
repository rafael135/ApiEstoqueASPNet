using ApiEstoqueASP.Data.DTOs;
using ApiEstoqueASP.Integration.Test.DataBuilders;
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
    public class Order_GET : IClassFixture<ApiEstoqueASPFactory>
    {
        private readonly ApiEstoqueASPFactory _app;

        public Order_GET()
        {
            this._app = new ApiEstoqueASPFactory();
        }

        [Fact]
        public async Task GET_Order_By_Id_Returns_Ok()
        {
            // Arrange
            using HttpClient client = await this._app.GetHttpClientWithAuthenticationTokenAsync();
            Order? existentOrder = this._app.Context.Orders.FirstOrDefault();

            if (existentOrder is null)
            {
                existentOrder = new OrderDataBuilder().Generate();
                this._app.Context.Orders.Add(existentOrder);
                this._app.Context.SaveChanges();
            }

            int orderId = existentOrder.Id;


            // Act
            var res = await client.GetFromJsonAsync<ReadOrderDto>($"/order/{orderId}");


            // Assert
            Assert.NotNull(res);
            Assert.IsType<ReadOrderDto>(res);
            Assert.Equal(existentOrder.TotalPrice, res.TotalPrice);
        }



        [Fact]
        public async Task GET_NonExistent_Order_By_Id_Returns_NotFound()
        {
            // Arrange
            using HttpClient client = await this._app.GetHttpClientWithAuthenticationTokenAsync();

            HttpStatusCode expectedStatus = HttpStatusCode.NotFound;

            // Act
            var res = await client.GetAsync("/order/98374733");


            // Assert
            Assert.NotNull(res);
            Assert.Equal(expectedStatus, res.StatusCode);
        }
    }
}
