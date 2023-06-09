﻿using AW3.GR.OpenAI.Domain.Common.Models;

namespace AW3.GR.OpenAI.Domain.SearchHistories.ValueObjects;

public class SearchHistoryId : AggregateRootId<Guid>
{
    private SearchHistoryId(Guid value) : base(value)
    {
    }

    public static SearchHistoryId CreateUnique() => new(Guid.NewGuid());

    public static SearchHistoryId Create(Guid value) => new(value);
}
