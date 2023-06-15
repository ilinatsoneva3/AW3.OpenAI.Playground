namespace AW3.GR.OpenAI.Domain.Common.Models;

public abstract class Entity<TId> : IEquatable<Entity<TId>>
    where TId : ValueObject
{
    public TId Id { get; protected set; }

    protected Entity()
    {
    }

    protected Entity(TId id)
    {
        Id = id;
    }

    public bool Equals(Entity<TId>? other)
    {
        return Equals((object?)other);
    }

    public override bool Equals(object? obj)
    {
        return obj is not null && obj.GetType() == GetType() && obj is Entity<TId> entity && Id.Equals(entity.Id);
    }

    public static bool operator ==(Entity<TId>? first, Entity<TId>? second)
    {
        return Equals(first, second);
    }

    public static bool operator !=(Entity<TId>? first, Entity<TId>? second)
    {
        return !Equals(first, second);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
