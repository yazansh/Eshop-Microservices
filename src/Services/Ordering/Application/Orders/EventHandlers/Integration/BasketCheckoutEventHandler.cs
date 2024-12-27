namespace Ordering.Application.Orders.EventHandlers.Integration;
public class BasketCheckoutEventHandler
    (ISender sender, ILogger<BasketCheckoutEventHandler> logger)
    : IConsumer<BasketCheckoutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        // Create new order 
        logger.LogInformation("Integration Event handled: {IntegrationEvent} - Integration Type : {ItegrationType}", 
            context.Message.GetType().Name, context.Message.EventType);

        var createOrderCommand = MapToCreateOrderCommand(context.Message);//context.Message.Adapt<CreateOrderCommand>();

        //start order fullfillment process (send domain event which raises an integration event)
        await sender.Send(createOrderCommand);
    }

    private static CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent message)
    {
        // Create full order with incoming event data
        var addressDto = new AddressDto(message.BillingAddress.FirstName, message.BillingAddress.LastName, message.BillingAddress.EmailAddress ?? ""
            , message.BillingAddress.AddressLine, message.BillingAddress.Country, message.BillingAddress.State, message.BillingAddress.ZipCode);
        var paymentDto = new PaymentDto(message.Payment.CardName, message.Payment.CardNumber, message.Payment.Expiration, message.Payment.CVV, message.Payment.PaymentMethod);
        var orderId = Guid.NewGuid();// not required I believe!

        var orderDto = new OrderDto(
            Id: orderId,
            CustomerId: message.CustomerId,
            OrderName: message.OrderName,
            ShippingAddress: addressDto,
            BillingAddress: addressDto,
            Payment: paymentDto,
            Status: OrderStatus.Pending,
            OrderItems: message.OrderItems.Select(i => new OrderItemDto(i.ProductId, i.Price, i.Quantity)).ToList());

        return new CreateOrderCommand(orderDto);
    }
}
