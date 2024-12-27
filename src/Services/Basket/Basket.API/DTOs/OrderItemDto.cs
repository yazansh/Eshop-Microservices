namespace Basket.API.DTOs;

public record OrderItemDto : BasketCheckoutEventOrderItem
{
    [SetsRequiredMembers]
    public OrderItemDto(Guid OrderId, Guid ProductId, decimal Price, int Quantity) : base(OrderId, ProductId, Price, Quantity)
    {
    }
}