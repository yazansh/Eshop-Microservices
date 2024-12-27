namespace Basket.API.DTOs;

public record BasketCheckoutDto
{
    public required string UserName { get; set; }
    public required Guid CustomerId { get; set; }
    public required AddressDto BillingAddress { get; set; }
    public required AddressDto ShippingAddress { get; set; }
    public required PaymentDto Payment { get; set; }
    public required IEnumerable<OrderItemDto> OrderItems { get; set; }
    //public  decimal TotalPrice { get; protected set; }
}