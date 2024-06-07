﻿using ApiEstoqueASP.Controllers;
using ApiEstoqueASP.Data.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;

namespace ApiEstoqueASP.Integration.Test
{
    public class TestAuthRegister : IDisposable
    {
        
        public TestAuthRegister()
        {
            
        }

        [Fact]
        public async Task POST_Register_Route_Returns_Created()
        {
            // Arrange
            var app = new ApiEstoqueASPFactory();
            HttpClient client = app.CreateClient();

            var data = new CreateUserDto()
            {
                Username = $"TestUser{new Random().Next(1, 999999)}",
                Email = $"test{new Random().Next(1, 999999)}@email.com",
                Password = "00000000Teste#",
                RePassword = "00000000Teste#"
            };
            HttpStatusCode expectedStatusCode = HttpStatusCode.Created;


            // Act
            HttpResponseMessage res = await client.PostAsJsonAsync("/auth/register", data);

            // Assert
            Assert.Equal(expectedStatusCode, res.StatusCode);
        }

        [Fact]
        public async Task POST_Register_Route_Returns_BadRequest()
        {
            // Arrange
            var app = new ApiEstoqueASPFactory();
            HttpClient client = app.CreateClient();

            var user = new CreateUserDto()
            {
                Username = "TestUser213123",
                Email = "",
                Password = "00000000Teste#",
                RePassword = "00000000Teste#"
            };
            HttpStatusCode expectedResponse = HttpStatusCode.BadRequest;


            // Act
            HttpResponseMessage res = await client.PostAsJsonAsync("/auth/register", user);

            // Assert
            Assert.Equal(expectedResponse, res.StatusCode);
        }

        public void Dispose()
        {
            
        }
    }
}
