using AW3.GR.OpenAI.Domain.AuthorAggregate.ValueObjects;
using AW3.GR.OpenAI.Domain.Common.Models;
using AW3.GR.OpenAI.Domain.Quotes;

namespace AW3.GR.OpenAI.Domain.Authors;

public class Author : AggregateRoot<AuthorId, Guid>
{
    private readonly List<Quote> _quotes = new();

    public string FirstName { get; private set; }

    public string? MiddleName { get; private set; }

    public string LastName { get; private set; }

    public IReadOnlyList<Quote> Quotes => _quotes.AsReadOnly();

    public void AddQuote(Quote quote)
        => _quotes.Add(quote);

    public string GetFullName()
        => string.IsNullOrWhiteSpace(MiddleName)
            ? $"{FirstName} {LastName}"
            : $"{FirstName} {MiddleName} {LastName}";

    public static Author Create(string firstName, string lastName, string? middleName)
        => new(AuthorId.CreateUnique(), firstName, lastName, middleName);

    private Author(AuthorId authorId, string firstName, string lastName, string? middleName) : base(authorId)
    {
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
    }

    private Author()
    {
    }
}
