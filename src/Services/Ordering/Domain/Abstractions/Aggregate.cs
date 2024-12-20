using System.Collections.ObjectModel;

namespace Domain.Abstractions;
public abstract class Aggregate<Tid> : Entity<Tid>, IAggregate<Tid>
{
    private readonly IList<IDomainEvent> _domainEvents = [];

    public ReadOnlyCollection<IDomainEvent> DomainEvents  => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public IEnumerable<IDomainEvent> ClearDomainEvents()
    {
        var domainEvents = _domainEvents.AsReadOnly();

        _domainEvents.Clear();

        return domainEvents;
    }
}
