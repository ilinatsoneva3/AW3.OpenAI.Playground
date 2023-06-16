using System.Text;
using AW3.GR.OpenAI.Application.Common.Interfaces.Authentication;
using AW3.GR.OpenAI.Application.Common.Interfaces.Repositories;
using AW3.GR.OpenAI.Application.Common.Interfaces.Services;
using AW3.GR.OpenAI.Application.Interfaces;
using AW3.GR.OpenAI.Infrastructure.Authentication;
using AW3.GR.OpenAI.Infrastructure.Clients;
using AW3.GR.OpenAI.Infrastructure.Persistence.Repositories;
using AW3.GR.OpenAI.Infrastructure.Services;
using AW3.GR.OpenAI.Infrastructure.Services.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AW3.GR.OpenAI.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.ConfigureAuthentication(configuration);

        services.AddOpenAi(settings =>
        {
            settings.ApiKey = configuration["OpenAI:ApiKey"];
        });
        services.AddScoped<IOpenAiClient, OpenAIClient>();

        services.Configure<PasswordHashingOptions>(
            configuration.GetSection(PasswordHashingOptions.TITLE));

        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IUserContextService, UserContextService>();

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }

    private static IServiceCollection ConfigureAuthentication(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);
        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtGenerator, JwtGenerator>();

        services.AddAuthentication().AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
            };
        });


        return services;
    }
}