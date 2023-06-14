using AW3.GR.OpenAI.Domain.Common.Models;

namespace AW3.GR.OpenAI.Domain.ValueObjects;

public class SearchHistoryId : AggregateRootId<Guid>
{
    public SearchHistoryId(Guid value) : base(value)
    {
    }

    public static SearchHistoryId CreateUnique()
    {
        return new SearchHistoryId(Guid.NewGuid());
    }

    public static SearchHistoryId Create(Guid searchHistoryId)
    {
        return new SearchHistoryId(searchHistoryId);
    }
}
