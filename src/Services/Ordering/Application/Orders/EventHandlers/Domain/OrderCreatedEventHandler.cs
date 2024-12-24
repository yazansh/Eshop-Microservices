namespace Ordering.Application.Orders.EventHandlers.Domain;
public class OrderCreatedEventHandler 
    (ILogger<OrderCreatedEventHandler> logger)
    : INotificationHandler<OrderCreatedEvent>
{
    public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Order Created Event Handler : {@Notification}", notification);

        return Task.CompletedTask;
    }
}
