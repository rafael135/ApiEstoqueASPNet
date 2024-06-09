using ApiEstoqueASP.Controllers;
using ApiEstoqueASP.Data.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace ApiEstoqueASP.Integration.Test
{
    public class AuthLogin : IClassFixture<ApiEstoqueASPFactory>
    {

        private readonly ApiEstoqueASPFactory _app;

        public AuthLogin()
        {
            this._app = new ApiEstoqueASPFactory();
        }

        [Fact]
        public async Task POST_Login_Route_Returns_Ok()
        {
            // Arrange
            HttpClient client = this._app.CreateClient();
            
            var data = new LoginUserDto() {
                Email = "user@email.com",
                Password = "00000000Teste#"
            };
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK;


            // Act
            HttpResponseMessage res = await client.PostAsJsonAsync("/auth/login", data);

            // Assert
            Assert.Equal(expectedStatusCode, res.StatusCode);
        }

        [Fact]
        public async Task POST_Login_Route_Returns_Unauthorized()
        {
            // Arrange
            HttpClient client = this._app.CreateClient();
            var data = new LoginUserDto()
            {
                Email = "emailinvalido@test.com",
                Password = "dsdadasdasdS#"
            };
            var expectedStatusCode = HttpStatusCode.Unauthorized;

            // Act
            HttpResponseMessage res = await client.PostAsJsonAsync("/auth/login", data);

            // Assert
            Assert.Equal(expectedStatusCode, res.StatusCode);
        }
    }
}