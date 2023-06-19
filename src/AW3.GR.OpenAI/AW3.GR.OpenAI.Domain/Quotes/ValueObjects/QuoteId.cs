﻿using AW3.GR.OpenAI.Domain.Common.Models;

namespace AW3.GR.OpenAI.Domain.Quotes.ValueObjects;

public class QuoteId : AggregateRootId<Guid>
{
    private QuoteId(Guid value) : base(value)
    {
    }

    public static QuoteId CreateUnique() => new(Guid.NewGuid());
}
