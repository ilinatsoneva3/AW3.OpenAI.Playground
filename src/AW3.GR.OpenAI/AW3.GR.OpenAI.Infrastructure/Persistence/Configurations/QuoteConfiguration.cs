using AW3.GR.OpenAI.Domain.AuthorAggregate.ValueObjects;
using AW3.GR.OpenAI.Domain.Quotes;
using AW3.GR.OpenAI.Domain.Quotes.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW3.GR.OpenAI.Infrastructure.Persistence.Configurations;

public class QuoteConfiguration : IEntityTypeConfiguration<Quote>
{
    public void Configure(EntityTypeBuilder<Quote> builder)
    {
        ConfigureQuote(builder);
    }

    private void ConfigureQuote(EntityTypeBuilder<Quote> builder)
    {
        builder.ToTable("Quotes");

        builder.HasKey(q => q.Id);

        builder.Property(q => q.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => QuoteId.Create(value));

        builder.Property(q => q.Content)
            .IsRequired();

        builder.Property(q => q.AuthorId)
            .HasConversion(
                id => id.Value,
                value => AuthorId.Create(value));
    }
}
