namespace Ordering.Application.Extensions;
public static class OrderExtensions
{
    public static IEnumerable<OrderDto> ToOrderDtoList(this IEnumerable<Order> orders)
    {
        return orders.Select(order => CreateOrderDto(order)).ToList();
    }

    public static OrderDto ToOrderDto(this Order order)
    {
        return CreateOrderDto(order);
    }

    private static OrderDto CreateOrderDto(Order order)
    {
        var id = order.Id.Value;
        var customerId = order.CustomerId.Value;
        var orderName = order.OrderName.Value;
        var billingAddress = new AddressDto(order.BillingAddress.FirstName, order.BillingAddress.LastName, order.BillingAddress.EmailAddress!, order.BillingAddress.AddressLine, order.BillingAddress.Country, order.BillingAddress.State, order.BillingAddress.ZipCode);
        var shippingAddress = new AddressDto(order.ShippingAddress.FirstName, order.ShippingAddress.LastName, order.ShippingAddress.EmailAddress!, order.ShippingAddress.AddressLine, order.ShippingAddress.Country, order.ShippingAddress.State, order.ShippingAddress.ZipCode);
        var payment = new PaymentDto(order.Payment.CardName, order.Payment.CardNumber, order.Payment.Expiration, order.Payment.CVV, order.Payment.PaymentMethod);
        var status = order.Status;
        var orderItems = order.OrderItems.Select(oi => new OrderItemDto(oi.ProductId.Value, oi.Price, oi.Quantity)).ToList();

        return new OrderDto(id, customerId, orderName, billingAddress, shippingAddress, payment, status, orderItems);
    }
}
