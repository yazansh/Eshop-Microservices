namespace Ordering.Application.Orders.Commands.CreateOrder;
public class CreateOrderHandler
    (IApplicationDbContext dbContext)
    : ICommandHandler<CreateOrderCommand, CreateOrderCommandResult>
{
    public async Task<CreateOrderCommandResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = ProjectOrderDtoToDomainOrder(command.Order);

        await dbContext.Orders.AddAsync(order, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateOrderCommandResult(order.Id.Value);
    }

    private Order ProjectOrderDtoToDomainOrder(OrderDto orderDto)
    {
        var order = Order.Create(
            OrderId.Of(Guid.NewGuid()),
            OrderName.Of(orderDto.OrderName),
            CustomerId.Of(orderDto.CustomerId),
            billingAddress: Address.Of(orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName, orderDto.BillingAddress.EmailAddress, orderDto.BillingAddress.AddressLine, orderDto.BillingAddress.Country, orderDto.BillingAddress.State, orderDto.BillingAddress.ZipCode),
            shippingAddress: Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName, orderDto.ShippingAddress.EmailAddress, orderDto.ShippingAddress.AddressLine, orderDto.ShippingAddress.Country, orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZipCode),
            Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration, orderDto.Payment.Cvv, orderDto.Payment.PaymentMethod)
            );

        orderDto.OrderItems.ForEach(oi => order.Add(ProductId.Of(oi.ProductId), oi.Price, oi.Quantity));

        return order;
    }
}
