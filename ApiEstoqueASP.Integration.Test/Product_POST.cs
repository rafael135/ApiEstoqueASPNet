using ApiEstoqueASP.Data.DTOs;
using ApiEstoqueASP.Integration.Test.DataBuilders;
using ApiEstoqueASP.Models;
using System.Net;
using System.Net.Http.Json;

namespace ApiEstoqueASP.Integration.Test
{
    public class Product_POST : IClassFixture<ApiEstoqueASPFactory>
    {
        private readonly ApiEstoqueASPFactory _app;
        public Product_POST()
        {
            this._app = new ApiEstoqueASPFactory();
        }

        [Fact]
        public async Task POST_Register_New_Product_Returns_Created()
        {
            // Arrange
            using HttpClient client = await this._app.GetHttpClientWithAuthenticationTokenAsync();

            Supplier? existentSupplier = this._app.Context.Suppliers.FirstOrDefault();
            if (existentSupplier is null)
            {
                existentSupplier = new SupplierDataBuilder().Generate();

                this._app.Context.Suppliers.Add(existentSupplier);
                this._app.Context.SaveChanges();
            }

            Product newProduct = new ProductDataBuilder().Generate();

            CreateProductDto data = new CreateProductDto()
            {
                Name = newProduct.Name,
                SupplierId = existentSupplier.Id,
                Price = newProduct.Price,
                InStock = newProduct.InStock
            };
            HttpStatusCode expectedResponse = HttpStatusCode.Created;

            // Act
            HttpResponseMessage res = await client.PostAsJsonAsync("/product", data);
            var product = await res.Content.ReadFromJsonAsync<ReadProductDto>();

            // Assert
            Assert.Equal(expectedResponse, res.StatusCode);
            Assert.NotNull(product);
            Assert.Equal(data.Name, product.Name);
            Assert.Equal(data.SupplierId, product.SupplierId);
            Assert.Equal(data.Price, product.Price, 0.001);
        }
    }
}
