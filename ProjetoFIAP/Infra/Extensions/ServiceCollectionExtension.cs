using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using ProjetoFIAP.Api.Infra.Data;
using ProjetoFIAP.Api.Infra.Repository;
using ProjetoFIAP.Api.Infra.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ProjetoFIAP.Api.Infra.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddMyAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine($"Falha na autenticação: {context.Exception.Message}");
                            return Task.CompletedTask;
                        }
                    };
                });

            return services;
        }

        public static IServiceCollection AddMyAuthorization(this IServiceCollection services)
        {
            services.AddAuthorizationBuilder()
                .AddPolicy("Admin", policy => 
                    policy.RequireAssertion(context =>
                    {
                        var user = context.User;
                        var hasRole = user.HasClaim(c => 
                            (c.Type == ClaimTypes.Role || c.Type == "role") && 
                            c.Value.Equals("Admin", StringComparison.OrdinalIgnoreCase));
                        
                        Console.WriteLine($"Verificando política Admin - HasRole: {hasRole}");
                        Console.WriteLine("Claims disponíveis:");
                        foreach (var claim in user.Claims)
                        {
                            Console.WriteLine($"Type: {claim.Type}, Value: {claim.Value}");
                        }
                        
                        return hasRole;
                    }));

            return services;
        }

        public static IServiceCollection AddMySwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "FIAP Games API",
                    Version = "v1",
                    Description = "API para gerenciamento de jogos e usuários",
                    Contact = new OpenApiContact
                    {
                        Name = "FIAP",
                        Email = "contato@fiap.com.br"
                    }
                });

                options.EnableAnnotations();

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization: Bearer {token}"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            return services;
        }

        public static IServiceCollection AddMyApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IGameRepository, GameRepository>();
            
            return services;
        }

        public static IServiceCollection AddMyInfra(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") 
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString, sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(5),
                        errorNumbersToAdd: null);
                });
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IGameRepository, GameRepository>();

            return services;
        }
    }
}
