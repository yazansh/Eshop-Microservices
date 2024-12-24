namespace Ordering.Application.Orders.Commands.UpdateOrder;
public class UpdateOrderHandler
    (IApplicationDbContext dbContext)
    : ICommandHandler<UpdateOrderCommand, UpdateOrderCommandResult>
{
    public async Task<UpdateOrderCommandResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders.FindAsync([OrderId.Of(command.OrderDto.Id)], cancellationToken)
            ?? throw new OrderNotFoundException(command.OrderDto.Id);
        
        UpdateOrder(command.OrderDto, order);

        dbContext.Orders.Update(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateOrderCommandResult(true);
    }

    private static void UpdateOrder(OrderDto orderDto, Order order)
    {
        order.Update(
            OrderName.Of(orderDto.OrderName),
            billingAddress: Address.Of(orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName, orderDto.BillingAddress.EmailAddress, orderDto.BillingAddress.AddressLine, orderDto.BillingAddress.Country, orderDto.BillingAddress.State, orderDto.BillingAddress.ZipCode),
            shippingAddress: Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName, orderDto.ShippingAddress.EmailAddress, orderDto.ShippingAddress.AddressLine, orderDto.ShippingAddress.Country, orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZipCode),
            Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration, orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod),
            status: orderDto.Status
            );
    }
}
