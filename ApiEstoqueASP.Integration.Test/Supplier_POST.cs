using ApiEstoqueASP.Data.DTOs;
using ApiEstoqueASP.Integration.Test.DataBuilders;
using ApiEstoqueASP.Models;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Http.Json;
using Xunit.Abstractions;

namespace ApiEstoqueASP.Integration.Test
{
    public class Supplier_POST : IClassFixture<ApiEstoqueASPFactory>
    {
        private readonly ApiEstoqueASPFactory _app;
        private readonly ITestOutputHelper _output;
        public Supplier_POST(ITestOutputHelper output)
        {
            this._output = output;
            this._app = new ApiEstoqueASPFactory();
        }

        [Fact]
        public async Task POST_Register_New_Supplier_Returns_Created()
        {
            // Arrange
            using var client = await _app.GetHttpClientWithAuthenticationTokenAsync();

            Supplier supplier = new SupplierDataBuilder().Generate();

            CreateSupplierDto data = new CreateSupplierDto()
            {
                Name = supplier.Name
            };
            HttpStatusCode expectedResponse = HttpStatusCode.Created;

            // Act
            HttpResponseMessage res = await client.PostAsJsonAsync("/supplier", data);
            
            
            var bodyJson = await res.Content.ReadFromJsonAsync<ReadSupplierDto>();

            // Assert
            Assert.Equal(expectedResponse, res.StatusCode);
            Assert.NotNull(bodyJson);
            Assert.Equal(data.Name, bodyJson.Name);
        }


        [Fact]
        public async Task POST_Register_New_Supplier_WithNoName_Returns_BadRequest()
        {
            // Arrange
            using var client = await _app.GetHttpClientWithAuthenticationTokenAsync();

            CreateSupplierDto data = new CreateSupplierDto()
            {
                Name = ""
            };
            HttpStatusCode expectedResponse = HttpStatusCode.BadRequest;


            // Act
            HttpResponseMessage res = await client.PostAsJsonAsync("/supplier", data);


            // Assert
            Assert.NotNull(res);
            Assert.Equal(expectedResponse, res.StatusCode);
        }


        [Fact]
        public async Task POST_Register_New_Supplier_WithLongName_Returns_BadRequest()
        {
            // Arrange
            using var client = await _app.GetHttpClientWithAuthenticationTokenAsync();

            CreateSupplierDto data = new CreateSupplierDto()
            {
                Name = "dusadhsaudhauihquidhquidhsaudhjauisdhsaiudhauihqwuoehqwueqwuihdasiudhasuidhasudihasduiashdasdsadadasdwqeqw"
            };
            HttpStatusCode expectedResponse = HttpStatusCode.BadRequest;


            // Act
            HttpResponseMessage res = await client.PostAsJsonAsync("/supplier", data);


            // Assert
            Assert.NotNull(res);
            Assert.Equal(expectedResponse, res.StatusCode);
        }
    }
}
