using AW3.GR.OpenAI.API.Common.Errors;
using AW3.GR.OpenAI.API.Common.Mapping;
using AW3.GR.OpenAI.API.Common.Middleware;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace AW3.GR.OpenAI.API;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddMappings();
        services.AddControllers();

        services.AddSingleton<ProblemDetailsFactory, AW3ProblemFactory>();

        services.AddTransient<GlobalExceptionHandlingMiddleware>();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}