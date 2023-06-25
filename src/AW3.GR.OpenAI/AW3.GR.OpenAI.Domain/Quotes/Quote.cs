using AW3.GR.OpenAI.Domain.AuthorAggregate.ValueObjects;
using AW3.GR.OpenAI.Domain.Common.Models;
using AW3.GR.OpenAI.Domain.Quotes.ValueObjects;

namespace AW3.GR.OpenAI.Domain.Quotes;

public class Quote : AggregateRoot<QuoteId, Guid>
{
    public string Content { get; private set; }

    public AuthorId AuthorId { get; private set; }

    public Quote(string content, AuthorId authorId) : base(QuoteId.CreateUnique())
    {
        Content = content;
        AuthorId = authorId;
    }

    public static Quote Create(string content, AuthorId authorId)
        => new(content, authorId);

    private Quote()
    {
    }
}
