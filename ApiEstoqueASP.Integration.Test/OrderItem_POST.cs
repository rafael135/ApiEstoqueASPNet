using ApiEstoqueASP.Data;
using ApiEstoqueASP.Data.DTOs;
using ApiEstoqueASP.Integration.Test.DataBuilders;
using ApiEstoqueASP.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using Xunit.Abstractions;

namespace ApiEstoqueASP.Integration.Test
{
    public class OrderItem_POST : IClassFixture<ApiEstoqueASPFactory>, IDisposable
    {
        private List<int> registeredOrderItems = new List<int>();
        private readonly ApiEstoqueASPFactory _app;
        private ITestOutputHelper _helper;
        public OrderItem_POST(ITestOutputHelper helper)
        {
            this._app = new ApiEstoqueASPFactory();
            this._helper = helper;
        }

        public void Dispose()
        {
            for(int i = 0; i < registeredOrderItems.Count; i++)
            {
                OrderItem orderItem = this._app.Context.OrderItems.First(or => or.Id == registeredOrderItems[i]);
                this._app.Context.OrderItems.Remove(orderItem);
                this._app.Context.SaveChanges();
            }
            GC.SuppressFinalize(this);
        }

        [Fact]
        public async Task POST_Register_New_OrderItem_Returns_Created()
        {
            // Arrange
            using HttpClient client = await this._app.GetHttpClientWithAuthenticationTokenAsync();

            Supplier? existentSupplier = this._app.Context.Suppliers.FirstOrDefault();

            if(existentSupplier is null)
            {
                existentSupplier = new SupplierDataBuilder().Generate();
                this._app.Context.Suppliers.Add(existentSupplier);
                this._app.Context.SaveChanges();
            }

            Product? existentProduct = this._app.Context.Products.FirstOrDefault(prod => prod.InStock >= 15);
            if(existentProduct is null)
            {
                existentProduct = new ProductDataBuilder()
                {
                    SupplierId = existentSupplier.Id
                }.Generate();
                this._app.Context.Products.Add(existentProduct);
                this._app.Context.SaveChanges();
            }

            CreateOrderItemDto data = new CreateOrderItemDto()
            {
                ProductId = existentProduct.Id,
                UserId = this._app.LoggedUserId!,
                Quantity = new Random().Next(1, 15)
            };
            HttpStatusCode expectedResponse = HttpStatusCode.Created;

            // Act
            HttpResponseMessage res = await client.PostAsJsonAsync("/orderItem", data);
            var bodyResponse = await res.Content.ReadFromJsonAsync<ReadOrderItemDto>();
            this._helper.WriteLine($"{res.Content}");

            // Assert
            Assert.Equal(expectedResponse, res.StatusCode);
            Assert.NotNull(bodyResponse);
        }
    }
}
