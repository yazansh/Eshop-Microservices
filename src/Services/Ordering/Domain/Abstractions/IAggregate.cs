using System.Collections.ObjectModel;

namespace Domain.Abstractions;

public interface IAggregate<T> : IAggregate, IEntity<T>
{

}

public interface IAggregate : IEntity
{
    ReadOnlyCollection<IDomainEvent> DomainEvents { get; }

    //void AddDomainEvent(IDomainEvent domainEvent);

    IEnumerable<IDomainEvent> ClearDomainEvents();
}
