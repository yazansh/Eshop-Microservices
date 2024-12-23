namespace Ordering.Application.Dtos;
public record OrderDto(
    string OrderName,
    Guid CustomerId,
    AddressDto BillingAddress,
    AddressDto ShippingAddress,
    PaymentDto Payment,
    List<OrderItemDto> OrderItems);
