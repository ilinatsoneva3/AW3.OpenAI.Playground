using AW3.GR.OpenAI.Domain.Common.Models;

namespace AW3.GR.OpenAI.Domain.Books.ValueObjects;

public class BookId : AggregateRootId<Guid>
{
    private BookId(Guid value) : base(value)
    {
    }

    public static BookId CreateUnique() => new(Guid.NewGuid());
}
