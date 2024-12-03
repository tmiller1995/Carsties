using Carsties.Core.Interfaces;

namespace Carsties.Core;

public abstract class Entity
{
    protected readonly List<IDomainEvent> DomainEvents = [];

    public Guid Id { get; private init; }

    protected Entity()
    {
    }

    protected Entity(Guid id)
    {
        Id = id;
    }

    public List<IDomainEvent> PopDomainEvents()
    {
        var copy = DomainEvents.ToList();
        DomainEvents.Clear();

        return copy;
    }
}