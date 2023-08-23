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

    public static Author Create(string firstName, string lastName, string? middleName, string? authorId = null)
        => new(string.IsNullOrEmpty(authorId) ? AuthorId.CreateUnique() : AuthorId.Create(authorId), firstName, lastName, middleName);

    private Author(AuthorId authorId, string firstName, string lastName, string? middleName) : base(authorId)
    {
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Author()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }
}
