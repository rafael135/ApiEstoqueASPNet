using ApiEstoqueASP.Data.DTOs;
using ApiEstoqueASP.Integration.Test.DataBuilders;
using ApiEstoqueASP.Models;
using System.Net;
using System.Net.Http.Json;

namespace ApiEstoqueASP.Integration.Test
{
    public class OrderItem_GET : IClassFixture<ApiEstoqueASPFactory>
    {
        private readonly ApiEstoqueASPFactory _app;

        public OrderItem_GET()
        {
            this._app = new ApiEstoqueASPFactory();
        }

        [Fact]
        public async Task GET_Existent_OrderItem_Returns_Ok()
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

            OrderItem? existentOrderItem = this._app.Context.OrderItems.FirstOrDefault();

            if (existentOrderItem is null)
            {
                existentOrderItem = new OrderItem()
                {
                    UserId = this._app.LoggedUserId!,
                    ProductId = existentProduct.Id,
                    Quantity = new Random().Next(1, 15),
                };

                this._app.Context.OrderItems.Add(existentOrderItem);
                this._app.Context.SaveChanges();
            }

            HttpStatusCode expectedStatus = HttpStatusCode.OK;


            // Act
            HttpResponseMessage response = await client.GetAsync($"orderItem/{existentOrderItem.Id}");
            ReadOrderItemDto orderItemResponse = await response.Content.ReadFromJsonAsync<ReadOrderItemDto>();



            // Assert
            Assert.NotNull(response);
            Assert.NotNull(orderItemResponse);
            Assert.Equal(expectedStatus, response.StatusCode);
            Assert.Equal(existentOrderItem.Quantity, orderItemResponse.Quantity);
            Assert.Equal(existentOrderItem.ProductId, orderItemResponse.ProductId);
            Assert.Equal(existentOrderItem.UserId, orderItemResponse.UserId);
        }


        [Fact]
        public async Task GET_NonExistent_OrderItem_Returns_NotFound()
        {
            // Arrange
            using HttpClient client = await this._app.GetHttpClientWithAuthenticationTokenAsync();

            HttpStatusCode expectedStatus = HttpStatusCode.NotFound;


            // Act
            HttpResponseMessage response = await client.GetAsync("orderItem/94729724");


            // Assert
            Assert.NotNull(response);
            Assert.Equal(expectedStatus, response.StatusCode);
        }
    }
}
