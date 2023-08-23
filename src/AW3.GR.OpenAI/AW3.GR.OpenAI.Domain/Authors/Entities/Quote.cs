using AW3.GR.OpenAI.Domain.Common.Models;
using AW3.GR.OpenAI.Domain.Quotes.ValueObjects;

namespace AW3.GR.OpenAI.Domain.Quotes;

public sealed class Quote : Entity<QuoteId>
{
    public string Content { get; private set; }

    public Quote(string content) : base(QuoteId.CreateUnique())
    {
        Content = content;
    }

    public static Quote Create(string content)
        => new(content);

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Quote()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }
}
