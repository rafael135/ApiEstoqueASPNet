using ApiEstoqueASP.Data.DTOs;
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

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "231283123");
            

            //SupplierDataBuilder supplierDataBuilder = new SupplierDataBuilder();

            //Supplier newSupplier = supplierDataBuilder.Generate();

            CreateSupplierDto data = new CreateSupplierDto()
            {
                Name = "dasdad",
                //RegisterDate = newSupplier.RegisterDate
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
    }
}
