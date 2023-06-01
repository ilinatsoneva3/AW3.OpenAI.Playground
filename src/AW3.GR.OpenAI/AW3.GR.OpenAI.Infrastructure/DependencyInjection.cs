using AW3.GR.OpenAI.Application.Common.Interfaces.Authentication;
using AW3.GR.OpenAI.Application.Common.Interfaces.Services;
using AW3.GR.OpenAI.Application.Common.Persistence.Interfaces;
using AW3.GR.OpenAI.Application.Interfaces;
using AW3.GR.OpenAI.Infrastructure.Authentication;
using AW3.GR.OpenAI.Infrastructure.Clients;
using AW3.GR.OpenAI.Infrastructure.Persistence.Repositories;
using AW3.GR.OpenAI.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AW3.GR.OpenAI.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtGenerator, JwtGenerator>();

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IOpenAiClient, OpenAIClient>();

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}