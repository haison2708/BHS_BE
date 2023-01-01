namespace BHS.Domain.SeedWork;

public abstract class Entity : IEntity
{
    protected Entity()
    {
        CreatedAt = DateTime.UtcNow;
    }

    public DateTime CreatedAt { get; set; }
}

public abstract class Entity<T> : Entity, IEntity<T>
{
    public virtual T Id { get; init; } = default!;
}