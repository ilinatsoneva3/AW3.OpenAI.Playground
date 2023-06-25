using AW3.GR.OpenAI.Domain.SearchHistories;
using AW3.GR.OpenAI.Domain.SearchHistories.ValueObjects;
using AW3.GR.OpenAI.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW3.GR.OpenAI.Infrastructure.Persistence.Configurations;

public class SearchHistoryConfiguration : IEntityTypeConfiguration<SearchHistory>
{
    public void Configure(EntityTypeBuilder<SearchHistory> builder)
    {
        ConfigureSearchHistory(builder);
    }

    private void ConfigureSearchHistory(EntityTypeBuilder<SearchHistory> builder)
    {
        builder.ToTable("SearchHistories");

        builder.HasKey(sh => sh.Id);

        builder.Property(builder => builder.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => SearchHistoryId.Create(value));

        builder.Property(sh => sh.SearchText)
            .IsRequired();

        builder.Property(sh => sh.SearchResult)
            .IsRequired();

        builder.Property(sh => sh.UserId)
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value));
    }
}
