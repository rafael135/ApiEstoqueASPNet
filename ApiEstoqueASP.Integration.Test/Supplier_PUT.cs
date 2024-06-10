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
    public class Supplier_PUT : IClassFixture<ApiEstoqueASPFactory>
    {
        private readonly ApiEstoqueASPFactory _app;

        public Supplier_PUT()
        {
            this._app = new ApiEstoqueASPFactory();
        }

        [Fact]
        public async Task PUT_Update_Existent_Supplier_Returns_NoContent()
        {
            // Arrange
            using HttpClient client = await this._app.GetHttpClientWithAuthenticationTokenAsync();

            Supplier existentSupplier = this._app.GetExistentSupplierOrCreate();

            UpdateSupplierDto data = new UpdateSupplierDto()
            {
                Name = existentSupplier.Name
            };
            data.Name = "Nome Alterado";

            HttpStatusCode expectedStatus = HttpStatusCode.NoContent;



            // Act
            HttpResponseMessage response = await client.PutAsJsonAsync<UpdateSupplierDto>($"/supplier/{existentSupplier.Id}", data);



            // Assert
            Assert.NotNull(response);
            Assert.Equal(expectedStatus, response.StatusCode);
        }

        [Fact]
        public async Task PUT_Update_NonExistent_Supplier_Returns_NotFound()
        {
            // Arrange
            using HttpClient client = await this._app.GetHttpClientWithAuthenticationTokenAsync();

            UpdateSupplierDto data = new UpdateSupplierDto()
            {
                Name = "Nome Alterado"
            };

            HttpStatusCode expectedStatus = HttpStatusCode.NotFound;



            // Act
            HttpResponseMessage response = await client.PutAsJsonAsync<UpdateSupplierDto>($"/supplier/{984947}", data);



            // Assert
            Assert.NotNull(response);
            Assert.Equal(expectedStatus, response.StatusCode);
        }
    }
}
