namespace Domain.Events;
public record OrderUpdatedEvent(Order Order) : IDomainEvent
{
}
