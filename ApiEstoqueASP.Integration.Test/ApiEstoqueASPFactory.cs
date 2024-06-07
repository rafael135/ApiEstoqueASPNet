using ApiEstoqueASP.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiEstoqueASP.Integration.Test
{
    // WebApplicationFactory => Classe herdada para configurar uma instancia da API em memória
    public class ApiEstoqueASPFactory : WebApplicationFactory<Program>
    {
        
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            string connectionString = "Server=localhost;Port=5306;database=ApiEstoque;user=rafael;password=3541;";
            /*
            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<ApiEstoqueDbContext>));

                services.AddDbContext<ApiEstoqueDbContext>(options =>
                    options.UseLazyLoadingProxies()
                    .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                );
            });*/

            base.ConfigureWebHost(builder);
        }

        protected override IHostBuilder? CreateHostBuilder()
        {
            return base.CreateHostBuilder();
        }
    }
}
