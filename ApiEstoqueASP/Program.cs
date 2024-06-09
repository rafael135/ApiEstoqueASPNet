
using ApiEstoqueASP.Data;
using ApiEstoqueASP.Models;
using ApiEstoqueASP.Repositories;
using ApiEstoqueASP.Repositories.Interfaces;
using ApiEstoqueASP.Services;
using ApiEstoqueASP.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;

//using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace ApiEstoqueASP
{
    public class Program
    {
        public static async Task CreateInitialUsers(WebApplication app)
        {
            IServiceScopeFactory scopedFactory = app.Services.GetService<IServiceScopeFactory>();
            using(IServiceScope scope = scopedFactory.CreateScope())
            {
                ISeedUserRoleInitial service = scope.ServiceProvider.GetService<ISeedUserRoleInitial>();
                await service.SeedRoles();
                await service.SeedUsers();
            }
        }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("MySqlConnection");

            // Add services to the container.

            // Contexto de conex�o com o banco de dados
            builder.Services.AddDbContext<ApiEstoqueDbContext>(
                opts =>
                    opts.UseLazyLoadingProxies()
                    .UseMySql(connectionString,
                        ServerVersion.AutoDetect(connectionString)
                    )
            );

            //
            builder.Services
                .AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApiEstoqueDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                        ClockSkew = TimeSpan.Zero
                    };
                });


            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            #region Repositories
                builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
                builder.Services.AddTransient<IProductRepository, ProductRepository>();
                builder.Services.AddTransient<ISupplierRepository, SupplierRepository>();
                builder.Services.AddTransient<IOrderItemRepository, OrderItemRepository>();
                builder.Services.AddTransient<IOrderRepository, OrderRepository>();
            #endregion

            #region Services
                builder.Services.AddScoped<IUserService, UserService>();
                builder.Services.AddScoped<ITokenService, TokenService>();
                builder.Services.AddScoped<IProductService, ProductService>();
                builder.Services.AddScoped<IOrderService, OrderService>();
                builder.Services.AddScoped<IUserService, UserService>();
                builder.Services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
            #endregion


            builder.Services.AddControllers().AddNewtonsoftJson(opts =>
                opts.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiEstoque", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                // Adiciona o padrão do token em uso na documentação
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                // Adiciona um requisito de segurança à documentação da API
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            // Função para criar roles e usuários de teste
            CreateInitialUsers(app).GetAwaiter().GetResult();

            app.Run();
        }
    }
}


public partial class Program { }