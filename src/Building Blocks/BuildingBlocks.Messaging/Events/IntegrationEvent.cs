namespace BuildingBlocks.Messaging.Events;
public interface IIntegrationEvent
{
    Guid EventId => Guid.NewGuid();
    DateTime OccurredOn => DateTime.UtcNow;
    string EventType => GetType().AssemblyQualifiedName;
}