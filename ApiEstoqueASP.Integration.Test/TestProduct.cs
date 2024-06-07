using ApiEstoqueASP.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ApiEstoqueASP.Integration.Test
{
    [Collection(nameof(AuthContextFixture))]
    public class TestProduct
    {
        private readonly HttpClient _client;
        public TestProduct(AuthContextFixture authContextFixture)
        {
            this._client = authContextFixture.Client;
        }

        [Fact]
        public async Task POST_Register_New_Product_Returns_Created()
        {
            // Arrange
            CreateProductDto data = new CreateProductDto()
            {
                Name = "Test Product",
                SupplierId = 1,
                Price = new Random().NextDouble() * 999.0
            };
            HttpStatusCode expectedResponse = HttpStatusCode.Created;

            // Act
            HttpResponseMessage res = await this._client.PostAsJsonAsync("/product", data);

            // Assert
            Assert.Equal(expectedResponse, res.StatusCode);
        }
    }
}
