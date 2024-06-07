using ApiEstoqueASP.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApiEstoqueASP.Integration.Test
{
    [Collection(nameof(AuthContextFixture))]
    public class TestOrder
    {
        private readonly HttpClient _client;

        public TestOrder(AuthContextFixture authContextFixture)
        {
            this._client = authContextFixture.Client;
        }


        [Fact]
        public async Task POST_Register_New_Order_Returns_Created()
        {
            // Arrange
            CreateOrderDto data = new CreateOrderDto()
            {
                UserId = "b9b67d0a-d760-4d63-8881-0e85544d2779"
            };
            HttpStatusCode expectedResponse = HttpStatusCode.Created;

            // Act
            HttpResponseMessage res = await this._client.PostAsJsonAsync("/order", data);

            // Assert
            Assert.Equal(expectedResponse, res.StatusCode);
        }

        [Fact]
        public async Task GET_Order_By_Id_Returns_Order()
        {
            // Arrange
            int orderId = 1;

            // Act
            var res = await this._client.GetFromJsonAsync<ReadOrderDto>($"/order/{orderId}");

            

            // Assert
            Assert.IsType<ReadOrderDto>(res);
        }
    }
}
