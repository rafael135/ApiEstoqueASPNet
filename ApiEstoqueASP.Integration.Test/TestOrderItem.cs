using ApiEstoqueASP.Data;
using ApiEstoqueASP.Data.DTOs;
using ApiEstoqueASP.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using Xunit.Abstractions;

namespace ApiEstoqueASP.Integration.Test
{
    [Collection(nameof(AuthContextFixture))]
    public class TestOrderItem : IDisposable
    {
        private readonly HttpClient _client;
        private readonly ApiEstoqueDbContext _context;
        private string _loggedUserId;
        private int _registeredOrderItemId;
        private ITestOutputHelper _helper;
        public TestOrderItem(AuthContextFixture authContextFixture, ITestOutputHelper helper)
        {
            this._client = authContextFixture.Client;
            this._context = authContextFixture.Context;
            this._loggedUserId = authContextFixture.UserId;
            this._helper = helper;
        }

        public void Dispose()
        {
            /*
            OrderItem? orderItem = this._context.OrderItems.Where(or => or.Id == this._registeredOrderItemId).FirstOrDefault();

            if(orderItem != null)
            {
                this._context.OrderItems.Remove(orderItem);
                this._context.SaveChanges();
            }
            */
            
        }

        [Fact]
        public async Task POST_Register_New_OrderItem_Returns_Created()
        {
            // Arrange
            CreateOrderItemDto data = new CreateOrderItemDto()
            {
                ProductId = 1,
                UserId = this._loggedUserId,
                Quantity = new Random().Next(1, 15)
            };
            HttpStatusCode expectedResponse = HttpStatusCode.Created;

            // Act
            HttpResponseMessage res = await this._client.PostAsJsonAsync("/orderItem", data);
            var content = await res.Content.ReadAsStringAsync();
            //this._helper.WriteLine(content);
            OrderItem? item = JsonConvert.DeserializeObject<OrderItem>(content);
            this._registeredOrderItemId = item.Id;

            // Assert
            Assert.Equal(expectedResponse, res.StatusCode);
        }
    }
}
