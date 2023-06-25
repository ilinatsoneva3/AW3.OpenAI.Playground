using AW3.GR.OpenAI.Domain.Authors;
using AW3.GR.OpenAI.Domain.Common.Models;
using AW3.GR.OpenAI.Domain.Quotes;
using AW3.GR.OpenAI.Domain.SearchHistories;
using AW3.GR.OpenAI.Domain.Users;
using AW3.GR.OpenAI.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using SmartEnum.EFCore;

namespace AW3.GR.OpenAI.Infrastructure.Persistence;

public class GROpenAIDbContext : DbContext
{
    private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor;
    public GROpenAIDbContext(
        DbContextOptions<GROpenAIDbContext> options,
        PublishDomainEventsInterceptor publishDomainEventsInterceptor) : base(options)
    {
        _publishDomainEventsInterceptor = publishDomainEventsInterceptor;
    }

    public DbSet<Author> Authors { get; set; } = null!;

    public DbSet<Quote> Quotes { get; set; } = null!;

    public DbSet<SearchHistory> SearchHistories { get; set; } = null!;

    public DbSet<User> Users { get; set; } = null!;

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.ConfigureSmartEnum();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_publishDomainEventsInterceptor);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Ignore<List<IDomainEvent>>()
            .ApplyConfigurationsFromAssembly(InfrastructureAssemblyReference.Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
