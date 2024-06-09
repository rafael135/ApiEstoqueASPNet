using ApiEstoqueASP.Data.DTOs;
using ApiEstoqueASP.Integration.Test.DataBuilders;
using ApiEstoqueASP.Models;
using System.Net;
using System.Net.Http.Json;

namespace ApiEstoqueASP.Integration.Test
{
    public class Product_GET : IClassFixture<ApiEstoqueASPFactory>
    {
        private readonly ApiEstoqueASPFactory _app;

        public Product_GET()
        {
            this._app = new ApiEstoqueASPFactory();
        }

        [Fact]
        public async Task GET_Existent_Product_Returns_Ok()
        {
            // Arrange
            using HttpClient client = await this._app.GetHttpClientWithAuthenticationTokenAsync();

            Product? existentProduct = this._app.Context.Products.FirstOrDefault();

            if (existentProduct is null)
            {
                Supplier? existentSupplier = this._app.Context.Suppliers.FirstOrDefault();

                if (existentSupplier is null)
                {
                    existentSupplier = new SupplierDataBuilder().Generate();
                    this._app.Context.Suppliers.Add(existentSupplier);
                    this._app.Context.SaveChanges();
                }

                existentProduct = new ProductDataBuilder()
                {
                    SupplierId = existentSupplier.Id
                }.Generate();
                this._app.Context.Products.Add(existentProduct);
                this._app.Context.SaveChanges();
            }
            HttpStatusCode expectedStatus = HttpStatusCode.OK;


            // Act
            HttpResponseMessage response = await client.GetAsync($"/product/{existentProduct.Id}");
            ReadProductDto? productResponse = await response.Content.ReadFromJsonAsync<ReadProductDto>();


            // Assert
            Assert.NotNull(response);
            Assert.NotNull(productResponse);
            Assert.Equal(expectedStatus, response.StatusCode);
            Assert.Equal(existentProduct.Name, productResponse.Name);
            Assert.Equal(existentProduct.Price, productResponse.Price);
            Assert.Equal(existentProduct.SupplierId, productResponse.SupplierId);
        }



        [Fact]
        public async Task GET_NonExistent_Product_Returns_NotFound()
        {
            // Arrange
            using HttpClient client = await this._app.GetHttpClientWithAuthenticationTokenAsync();
            HttpStatusCode expectedStatus = HttpStatusCode.NotFound;


            // Act
            HttpResponseMessage response = await client.GetAsync("/product/9384937");


            // Assert
            Assert.NotNull(response);
            Assert.Equal(expectedStatus, response.StatusCode);
        }
    }
}
