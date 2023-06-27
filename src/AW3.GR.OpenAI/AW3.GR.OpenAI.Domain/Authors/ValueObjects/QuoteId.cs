using AW3.GR.OpenAI.Domain.Common.Models;

namespace AW3.GR.OpenAI.Domain.Quotes.ValueObjects;

public sealed class QuoteId : EntityId<Guid>
{
    private QuoteId(Guid value) : base(value)
    {
    }

    public static QuoteId CreateUnique() => new(Guid.NewGuid());

    public static QuoteId Create(Guid value) => new(value);
}
