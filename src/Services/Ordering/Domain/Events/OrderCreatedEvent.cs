﻿namespace Domain.Events;
public record OrderCreatedEvent(Order Order) : IDomainEvent
{
}
