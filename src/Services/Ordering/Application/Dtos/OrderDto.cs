namespace Ordering.Application.Dtos;
public record OrderDto(
    Guid Id,
    string OrderName,
    Guid CustomerId,
    AddressDto BillingAddress,
    AddressDto ShippingAddress,
    PaymentDto Payment,
    List<OrderItemDto> OrderItems);
