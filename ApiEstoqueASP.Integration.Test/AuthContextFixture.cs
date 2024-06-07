using ApiEstoqueASP.Data;
using ApiEstoqueASP.Data.DTOs;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ApiEstoqueASP.Integration.Test
{
    public class AuthContextFixture : IAsyncLifetime
    {
        public HttpClient Client { get; private set; }
        public ApiEstoqueDbContext Context { get; private set; }
        public string UserId { get; private set; }
        public AuthContextFixture()
        {
            
        }

        public async Task DisposeAsync()
        {
            
        }

        public async Task InitializeAsync()
        {
            ApiEstoqueASPFactory appFactory = new ApiEstoqueASPFactory();
            this.Client = appFactory.CreateClient();

            using(IServiceScope scope = appFactory.Services.CreateScope())
            {
                this.Context = scope.ServiceProvider.GetRequiredService<ApiEstoqueDbContext>();
            }

            LoginUserDto data = new LoginUserDto()
            {
                Email = "user@localhost",
                Password = "00000000Teste#"
            };
            HttpResponseMessage res = await this.Client.PostAsJsonAsync("/auth/login", data);

            ReadUserDto readUserDto = await res.Content.ReadFromJsonAsync<ReadUserDto>();

            this.UserId = readUserDto.Id;
            this.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", readUserDto.Token);
        }
    }
}
