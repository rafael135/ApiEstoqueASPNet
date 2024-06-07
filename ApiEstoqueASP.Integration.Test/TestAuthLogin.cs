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
    public class TestAuthLogin : IClassFixture<ApiEstoqueASPFactory>
    {

        [Fact]
        public async Task POST_Login_Route_Returns_Ok()
        {
            // Arrange
            var app = new ApiEstoqueASPFactory();
            HttpClient client = app.CreateClient();
            
            var data = new LoginUserDto() {
                Email = "user@localhost",
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
            var app = new ApiEstoqueASPFactory();
            HttpClient client = app.CreateClient();
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