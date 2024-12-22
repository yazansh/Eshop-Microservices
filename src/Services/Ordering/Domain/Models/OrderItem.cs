using Ordering.Domain.Abstractions;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Models;
public class OrderItem : Entity<OrderItemId>
{
    public OrderId OrderId { get; private set; } = default!;
    public ProductId ProductId { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;
    public int Quantity { get; private set; } = default!;


    public static OrderItem Create(OrderId orderId, ProductId productId, decimal price, int quantity)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(price, 0);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(quantity, 0);

        return new OrderItem
        {
            Id = OrderItemId.Of(Guid.NewGuid()),
            OrderId = orderId,
            ProductId = productId,
            Price = price,
            Quantity = quantity
        };
    }
}
