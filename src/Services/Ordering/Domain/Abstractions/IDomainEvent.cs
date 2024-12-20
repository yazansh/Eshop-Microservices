using MediatR;

namespace Domain.Abstractions;
public interface IDomainEvent : INotification
{
    public Guid Id => Guid.NewGuid();

    public DateTime OccurredOn => DateTime.Now;

    public string EventType => GetType().AssemblyQualifiedName;
}
