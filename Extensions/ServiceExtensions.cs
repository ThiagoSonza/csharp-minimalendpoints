using System.Text;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MinimalEndpoints.Authorization;
using MinimalEndpoints.Authorization.Handlers;
using MinimalEndpoints.Services;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MinimalEndpoints.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AdicionaAutenticacao(this IServiceCollection services)
    {
        services
            .AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "MinimalApi",
                    ValidAudience = "MinimalApi",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("b4f2e725b7a04e658953ea0d8be670e95a4cfe67e8a94f2c9617c2761e26d8f5"))
                };
            });

        services.AddAuthorizationBuilder()
            .AddPolicy("IdadeMinimaRequirement", policy =>
            {
                policy.Requirements.Add(new IdadeMinimaRequirement(18));
            });

        return services;
    }

    public static IServiceCollection AdicionaVersionamento(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
            options.AssumeDefaultVersionWhenUnspecified = true;
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });
        return services;
    }

    public static IServiceCollection AdicionaSwagger(this IServiceCollection services)
    {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddSwaggerGen(options =>
        {
            // options.OperationFilter<SwaggerDefaultValues>();

            // Define o esquema de segurança
            options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                Description = "Insira 'Bearer' seguido do token JWT. Exemplo: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
            });

            // Aplica o esquema de segurança globalmente
            options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
            {
                {
                    new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                        Reference = new Microsoft.OpenApi.Models.OpenApiReference
                        {
                            Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        return services;
    }

    public static IServiceCollection AdicionaDependencias(this IServiceCollection services)
    {
        services.AddSingleton<TokenService>();
        services.AddSingleton<IAuthorizationHandler, IdadeMinimaHandler>();
        services.AddSingleton<IAuthorizationHandler, GrupoHandler>();
        return services;
    }
}