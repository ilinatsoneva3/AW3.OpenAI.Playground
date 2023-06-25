using AW3.GR.OpenAI.Domain.AuthorAggregate.ValueObjects;
using AW3.GR.OpenAI.Domain.Books.ValueObjects;
using AW3.GR.OpenAI.Domain.Common.Models;
using AW3.GR.OpenAI.Domain.Quotes.ValueObjects;

namespace AW3.GR.OpenAI.Domain.Books;

public class Book : AggregateRoot<BookId, Guid>
{
    private readonly List<QuoteId> _quoteIds = new();

    public string Title { get; private set; }

    public string? Description { get; private set; }

    public AuthorId AuthorId { get; private set; }

    public IReadOnlyList<QuoteId> QuoteIds => _quoteIds.AsReadOnly();

    public Book(string title, string? description, AuthorId authorId) : base(BookId.CreateUnique())
    {
        Title = title;
        Description = description;
        AuthorId = authorId;
    }

    public void AddDescription(string description)
    {
        Description = description;
    }

    public static Book Create(string title, string? description, AuthorId authorId)
        => new(title, description, authorId);

    private Book()
    {
    }
}
