using AW3.GR.OpenAI.Domain.Authors;
using AW3.GR.OpenAI.Domain.SearchHistories;
using AW3.GR.OpenAI.Domain.Users;
using Microsoft.EntityFrameworkCore;
using SmartEnum.EFCore;

namespace AW3.GR.OpenAI.Infrastructure.Persistence;

public class GROpenAIDbContext : DbContext
{
    public GROpenAIDbContext(DbContextOptions<GROpenAIDbContext> options) : base(options)
    {
    }

    public DbSet<Author> Authors { get; set; } = null!;

    public DbSet<SearchHistory> SearchHistories { get; set; } = null!;

    public DbSet<User> Users { get; set; } = null!;

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.ConfigureSmartEnum();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(InfrastructureAssemblyReference.Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
