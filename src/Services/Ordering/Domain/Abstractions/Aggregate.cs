namespace Ordering.Domain.Abstractions;
public abstract class Aggregate<Tid> : Entity<Tid>, IAggregate<Tid>
{
    private readonly IList<IDomainEvent> _domainEvents = [];
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public IDomainEvent[] ClearDomainEvents()
    {
        var domainEvents = _domainEvents.ToArray();

        _domainEvents.Clear();

        return domainEvents;
    }
}
