using AW3.GR.OpenAI.Domain.AuthorAggregate.ValueObjects;
using AW3.GR.OpenAI.Domain.Authors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW3.GR.OpenAI.Infrastructure.Persistence.Configurations;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        ConfigureAuthor(builder);
        ConfigureAuthorQuotes(builder);
    }

    private void ConfigureAuthor(EntityTypeBuilder<Author> builder)
    {
        builder.ToTable("Authors");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => AuthorId.Create(value));

        builder.Property(a => a.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(a => a.MiddleName)
            .HasMaxLength(50)
            .HasDefaultValue(null);

        builder.Property(a => a.LastName)
            .HasMaxLength(50)
            .IsRequired();
    }

    private void ConfigureAuthorQuotes(EntityTypeBuilder<Author> builder)
    {
        builder.OwnsMany(a => a.QuoteIds, q =>
        {
            q.ToTable("AuthorQuoteIds");

            q.WithOwner().HasForeignKey("AuthorId");

            q.HasKey("Id");

            q.Property(q => q.Value)
                .HasColumnName("QuoteId")
                .ValueGeneratedNever();
        });

        builder.Metadata
            .FindNavigation(nameof(Author.QuoteIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
