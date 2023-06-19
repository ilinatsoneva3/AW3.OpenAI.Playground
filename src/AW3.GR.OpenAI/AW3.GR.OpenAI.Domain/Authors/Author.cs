using AW3.GR.OpenAI.Domain.AuthorAggregate.ValueObjects;
using AW3.GR.OpenAI.Domain.Books.ValueObjects;
using AW3.GR.OpenAI.Domain.Common.Models;
using AW3.GR.OpenAI.Domain.Quotes.ValueObjects;

namespace AW3.GR.OpenAI.Domain.Authors;

public class Author : AggregateRoot<AuthorId, Guid>
{
    private readonly List<BookId> _books = new();
    private readonly List<QuoteId> _quotes = new();

    public string FirstName { get; private set; }

    public string? MiddleName { get; private set; }

    public string LastName { get; private set; }

    public IReadOnlyList<BookId> BookIds => _books.AsReadOnly();

    public IReadOnlyList<QuoteId> QuoteIds => _quotes.AsReadOnly();

    public void AddBook(BookId bookId)
        => _books.Add(bookId);

    public void AddQuote(QuoteId quoteId)
        => _quotes.Add(quoteId);

    public static Author Create(string firstName, string lastName, string? middleName)
        => new(firstName, lastName, middleName);

    private Author(string firstName, string lastName, string? middleName) : base(AuthorId.CreateUnique())
    {
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
    }

    private Author()
    {
    }
}
