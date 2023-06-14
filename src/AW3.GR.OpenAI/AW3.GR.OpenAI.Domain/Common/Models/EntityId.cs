namespace AW3.GR.OpenAI.Domain.Common.Models;

public abstract class EntityId<TId> : ValueObject
{
    public TId Id { get; }

    protected EntityId()
    {
    }

    protected EntityId(TId id)
    {
        Id = id;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Id;
    }

    public override string? ToString() => Id?.ToString() ?? base.ToString();
}
