using AW3.GR.OpenAI.Domain.Common.Models;

namespace AW3.GR.OpenAI.Domain.AuthorAggregate.ValueObjects;

public class AuthorId : AggregateRootId<Guid>
{
    private AuthorId(Guid value) : base(value)
    {
    }

    public static AuthorId CreateUnique() => new(Guid.NewGuid());

    public static AuthorId Create(Guid value) => new(value);

    public static AuthorId Create(string value) => new(Guid.Parse(value));
}
