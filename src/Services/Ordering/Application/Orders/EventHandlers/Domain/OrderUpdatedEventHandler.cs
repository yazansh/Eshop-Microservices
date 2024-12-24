namespace Ordering.Application.Orders.EventHandlers.Domain;
public class OrderUpdatedEventHandler 
    (ILogger<OrderUpdatedEventHandler> logger)
    : INotificationHandler<OrderUpdatedEvent>
{
    public Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Order Updated Event Handler: {@Notification}", notification);

        return Task.CompletedTask;
    }
}
