using System.Text;
using AW3.GR.OpenAI.Application.Common.Interfaces.Authentication;
using AW3.GR.OpenAI.Application.Common.Interfaces.Repositories;
using AW3.GR.OpenAI.Application.Common.Interfaces.Services;
using AW3.GR.OpenAI.Application.Interfaces;
using AW3.GR.OpenAI.Infrastructure.Authentication;
using AW3.GR.OpenAI.Infrastructure.Clients;
using AW3.GR.OpenAI.Infrastructure.Persistence;
using AW3.GR.OpenAI.Infrastructure.Persistence.Interceptors;
using AW3.GR.OpenAI.Infrastructure.Persistence.Repositories;
using AW3.GR.OpenAI.Infrastructure.Services;
using AW3.GR.OpenAI.Infrastructure.Services.Settings;
using Microsoft.EntityFrameworkCore;
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
        services
            .ConfigureAuthentication(configuration)
            .ConfigureClients(configuration)
            .AddPersistence(configuration);

        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<GROpenAIDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<PublishDomainEventsInterceptor>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ISearchHistoryRepository, SearchHistoryRepository>();

        return services;
    }

    private static IServiceCollection ConfigureAuthentication(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.Configure<PasswordHashingOptions>(
            configuration.GetSection(PasswordHashingOptions.TITLE));
        services.AddTransient<IPasswordHasher, PasswordHasher>();

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
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                ClockSkew = TimeSpan.FromSeconds(5)
            };
        });


        return services;
    }

    private static IServiceCollection ConfigureClients(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddOpenAi(settings =>
        {
            settings.ApiKey = configuration["OpenAI:ApiKey"];
        });
        services.AddScoped<IOpenAiClient, OpenAIClient>();
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IUserContextService, UserContextService>();
        return services;
    }
}