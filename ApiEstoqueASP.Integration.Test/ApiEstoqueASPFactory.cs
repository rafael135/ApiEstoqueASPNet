using ApiEstoqueASP.Data;
using ApiEstoqueASP.Data.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net;
using ApiEstoqueASP.Models;
using ApiEstoqueASP.Integration.Test.DataBuilders;

namespace ApiEstoqueASP.Integration.Test
{
    // WebApplicationFactory => Classe herdada para configurar uma instancia da API em memória
    public class ApiEstoqueASPFactory : WebApplicationFactory<Program>
    {
        private IServiceScope _scope;

        public ApiEstoqueDbContext Context { get; private set; }
        public string? LoggedUserId { get; private set; }

        public ApiEstoqueASPFactory()
        {
            this._scope = this.Services.CreateScope();

            this.Context = this._scope.ServiceProvider.GetRequiredService<ApiEstoqueDbContext>();
        }

        public async Task<HttpClient> GetHttpClientWithAuthenticationTokenAsync()
        {
            HttpClient client = this.CreateClient();

            var data = new LoginUserDto()
            {
                Email = "user@email.com",
                Password = "00000000Teste#"
            };


            HttpResponseMessage response = await client.PostAsJsonAsync("/auth/login", data);

            var result = await response.Content.ReadFromJsonAsync<ReadUserDto>();

            this.LoggedUserId = result!.Id;

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result!.Token);

            return client;
        }

        public Supplier GetExistentSupplierOrCreate()
        {
            Supplier? existentSupplier = this.Context.Suppliers.FirstOrDefault();

            if (existentSupplier is null)
            {
                existentSupplier = new SupplierDataBuilder().Generate();
                this.Context.Suppliers.Add(existentSupplier);
                this.Context.SaveChanges();
            }

            return existentSupplier;
        }

        public Product GetExistentProductOrCreate()
        {
            Product? existentProduct = this.Context.Products.FirstOrDefault();

            if (existentProduct is null)
            {
                Supplier supplier = this.GetExistentSupplierOrCreate();

                existentProduct = new ProductDataBuilder()
                {
                    SupplierId = supplier.Id
                }.Generate();
            }

            return existentProduct;
        }

        public OrderItem GetExistentOrderItemOrCreate()
        {
            OrderItem? existentOrderItem = this.Context.OrderItems.FirstOrDefault();

            if(existentOrderItem is null)
            {
                Product product = this.GetExistentProductOrCreate();

                existentOrderItem = new OrderItem()
                {
                    UserId = this.LoggedUserId!,
                    ProductId = product.Id,
                    Quantity = new Random().Next(1, 15),
                };

                this.Context.OrderItems.Add(existentOrderItem);
                this.Context.SaveChanges();
            }

            return existentOrderItem;
        }

        protected override void Dispose(bool disposing)
        {
            _scope.Dispose();

            base.Dispose(disposing);
        }
    }
}
