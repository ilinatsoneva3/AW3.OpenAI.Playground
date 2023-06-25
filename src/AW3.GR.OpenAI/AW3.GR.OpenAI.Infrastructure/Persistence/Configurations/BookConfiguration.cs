using AW3.GR.OpenAI.Domain.AuthorAggregate.ValueObjects;
using AW3.GR.OpenAI.Domain.Books;
using AW3.GR.OpenAI.Domain.Books.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AW3.GR.OpenAI.Infrastructure.Persistence.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        ConfigureBook(builder);
        ConfigureBookQuotes(builder);
    }

    private void ConfigureBookQuotes(EntityTypeBuilder<Book> builder)
    {
        builder.OwnsMany(b => b.QuoteIds, q =>
        {
            q.ToTable("BookQuotes");

            q.WithOwner().HasForeignKey("BookId");

            q.HasKey("Id");

            q.Property(q => q.Value)
                .HasColumnName("QuoteId")
                .ValueGeneratedNever();
        });
    }

    private void ConfigureBook(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Books");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => BookId.Create(value));

        builder.Property(b => b.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(b => b.AuthorId)
            .HasConversion(
                id => id.Value,
                value => AuthorId.Create(value));
    }
}
