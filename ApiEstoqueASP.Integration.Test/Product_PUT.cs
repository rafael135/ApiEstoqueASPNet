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
    public class Product_PUT : IClassFixture<ApiEstoqueASPFactory>
    {
        private readonly ApiEstoqueASPFactory _app;

        public Product_PUT()
        {
            this._app = new ApiEstoqueASPFactory();
        }

        [Fact]
        public async Task PUT_Update_Product_Returns_NoContent()
        {
            // Arrange
            using HttpClient client = await this._app.GetHttpClientWithAuthenticationTokenAsync();
            Product existentProduct = this._app.GetExistentProductOrCreate();

            UpdateProductDto data = new UpdateProductDto()
            {
                Name = existentProduct.Name,
                Price = new Random().NextDouble() * 200
            };
            HttpStatusCode expectedStatus = HttpStatusCode.NoContent;

            // Act
            HttpResponseMessage response = await client.PutAsJsonAsync<UpdateProductDto>($"/product/{existentProduct.Id}", data);


            // Assert
            Assert.NotNull(response);
            Assert.Equal(expectedStatus, response.StatusCode);
        }

        [Fact]
        public async Task PUT_Update_NonExistent_Product_Returns_NotFound()
        {
            // Arrange
            using HttpClient client = await this._app.GetHttpClientWithAuthenticationTokenAsync();

            UpdateProductDto data = new UpdateProductDto()
            {
                Name = "Teste",
                Price = new Random().NextDouble() * 200
            };
            HttpStatusCode expectedStatus = HttpStatusCode.NotFound;

            // Act
            HttpResponseMessage response = await client.PutAsJsonAsync<UpdateProductDto>("/product/99884732", data);


            // Assert
            Assert.NotNull(response);
            Assert.Equal(expectedStatus, response.StatusCode);
        }
    }
}
