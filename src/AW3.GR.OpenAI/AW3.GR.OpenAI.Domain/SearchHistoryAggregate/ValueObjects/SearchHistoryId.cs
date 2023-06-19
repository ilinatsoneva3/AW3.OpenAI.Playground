using AW3.GR.OpenAI.Domain.Common.Models;

namespace AW3.GR.OpenAI.Domain.SearchHistoryAggregate.ValueObjects;

public class SearchHistoryId : AggregateRootId<Guid>
{
    private SearchHistoryId(Guid value) : base(value)
    {
    }

    public static SearchHistoryId CreateUnique() => new(Guid.NewGuid());
}
