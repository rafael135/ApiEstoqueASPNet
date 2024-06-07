using ApiEstoqueASP.Data.DTOs;
using Microsoft.AspNetCore.Http;
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
    public class TestSupplier
    {
        private readonly HttpClient _client;
        public TestSupplier(AuthContextFixture authContextFixture)
        {
            this._client = authContextFixture.Client;
        }

        [Fact]
        public async Task POST_Register_New_Supplier_Returns_Created()
        {
            // Arrange
            CreateSupplierDto data = new CreateSupplierDto()
            {
                Name = $"Test{new Random().Next(1, 999999)}"
            };
            HttpStatusCode expectedResponse = HttpStatusCode.Created;

            // Act
            HttpResponseMessage res = await this._client.PostAsJsonAsync("/supplier", data);

            // Assert
            Assert.Equal(expectedResponse, res.StatusCode);
        }
    }
}
