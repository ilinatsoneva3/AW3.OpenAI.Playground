using AW3.GR.OpenAI.Application.Common.Interfaces.Authentication;
using AW3.GR.OpenAI.Application.Interfaces;
using AW3.GR.OpenAI.Infrastructure.Authentication;
using AW3.GR.OpenAI.Infrastructure.Clients;
using Microsoft.Extensions.DependencyInjection;

namespace AW3.GR.OpenAI.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IJwtGenerator, JwtGenerator>();

        services.AddScoped<IOpenAiClient, OpenAIClient>();

        return services;
    }
}
