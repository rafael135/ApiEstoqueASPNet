using ApiEstoqueASP.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ApiEstoqueASP.Models;
using ApiEstoqueASP.Integration.Test.DataBuilders;

namespace ApiEstoqueASP.Integration.Test
{
    public class Order_POST : IClassFixture<ApiEstoqueASPFactory>
    {
        private readonly ApiEstoqueASPFactory _app;

        public Order_POST()
        {
            this._app = new ApiEstoqueASPFactory();
        }


        [Fact]
        public async Task POST_Register_New_Order_Returns_Created()
        {
            // Arrange
            using HttpClient client = await this._app.GetHttpClientWithAuthenticationTokenAsync();
            CreateOrderDto data = new CreateOrderDto()
            {
                UserId = this._app.LoggedUserId
            };
            HttpStatusCode expectedResponse = HttpStatusCode.Created;

            // Act
            HttpResponseMessage res = await client.PostAsJsonAsync("/order", data);

            // Assert
            Assert.Equal(expectedResponse, res.StatusCode);
        }
    }
}
