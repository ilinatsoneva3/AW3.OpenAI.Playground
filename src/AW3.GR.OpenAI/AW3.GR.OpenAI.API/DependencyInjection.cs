using System.Text.Json.Serialization;
using AW3.GR.OpenAI.API.Common.Errors;
using AW3.GR.OpenAI.API.Common.Middleware;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.OpenApi.Models;

namespace AW3.GR.OpenAI.API;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers()
                .AddJsonOptions(options =>
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        services.ConfigureSwagger();

        services.AddSingleton<ProblemDetailsFactory, AW3ProblemFactory>();
        services.AddTransient<GlobalExceptionHandlingMiddleware>();

        services.AddEndpointsApiExplorer();

        return services;
    }

    private static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "AW3.GR.OpenAi",
                Version = "v1",
                Description = "Playground with ChatGPT and GoodReads APIs",
            });

            c.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new List<string>()
                }
            });

            c.EnableAnnotations();
        });

        return services;
    }
}