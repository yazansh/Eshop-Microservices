namespace Domain.Models;
public class Order : Aggregate<OrderId>
{
    private readonly List<OrderItem> _orderItems = [];

    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
    public OrderName OrderName { get; private set; } = default!;
    public CustomerId CustomerId { get; private set; } = default!;
    public Address BillingAddress { get; private set; } = default!;
    public Address ShippingAddress { get; private set; } = default!;
    public Payment Payment { get; private set; } = default!;
    public OrderStatus Status{ get; private set; }
    public decimal Total
    {
        get => _orderItems.Select(oi => oi.Quantity * oi.Price).Sum();
        private set { }
    }

    public static Order Create(OrderName orderName, CustomerId customerId, Address billingAddress, Address shippingAddress, Payment payment)
    {
        var order = new Order
        {
            Id = OrderId.Of(Guid.NewGuid()),
            OrderName = orderName,
            CustomerId = customerId,
            BillingAddress = billingAddress,
            ShippingAddress = shippingAddress,
            Payment = payment,
            Status = OrderStatus.Pending,
        };

        order.AddDomainEvent(new OrderCreatedEvent(order));

        return order;
    }

    public void Update(OrderName orderName, CustomerId customerId, Address billingAddress, Address shippingAddress, Payment payment, OrderStatus status)
    {
        OrderName = orderName;
        CustomerId = customerId;
        BillingAddress = billingAddress;
        ShippingAddress = shippingAddress;
        Payment = payment;
        Status = status;

        AddDomainEvent(new OrderUpdatedEvent(this));
    }

    public void Add(ProductId productId, decimal price, int quantity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

        var orderItem = OrderItem.Create(Id, productId, price, quantity);

        _orderItems.Add(orderItem);
    }

    public void Remove(ProductId productId)
    {
        var orderItem = _orderItems.FirstOrDefault(oi => oi.ProductId.Equals(productId));
        if (orderItem is not null)
        {
            _orderItems.Remove(orderItem);
        }
    }
}
