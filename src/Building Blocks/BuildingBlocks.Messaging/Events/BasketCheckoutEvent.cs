namespace BuildingBlocks.Messaging.Events;
public class BasketCheckoutEvent : IIntegrationEvent
{
    public required string OrderName { get; set; }
    public required Guid CustomerId { get; set; }
    public required BasketCheckoutEventAddress BillingAddress { get; set; }
    public required BasketCheckoutEventAddress ShippingAddress { get; set; }
    public required BasketCheckoutEventPayment Payment { get; set; }
    public required IEnumerable<BasketCheckoutEventOrderItem> OrderItems { get; set; }

    //public required OrderStatus Status { get; set; }
    //public  decimal TotalPrice { get; protected set; }
}

public class BasketCheckoutEventOrderItem
{
    public Guid OrderId { get; private set; } = default!;
    public Guid ProductId { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;
    public int Quantity { get; private set; } = default!;
}

public record BasketCheckoutEventPayment
{
    public string CardName { get; protected set; } = default!;
    public string CardNumber { get; protected set; } = default!;
    public string Expiration { get; protected set; } = default!;
    public string CVV { get; protected set; } = default!;
    public int PaymentMethod { get; protected set; } = default!;
}

public record BasketCheckoutEventAddress
{
    public string FirstName { get; protected set; } = default!;
    public string LastName { get; protected set; } = default!;
    public string? EmailAddress { get; protected set; } = default!;
    public string AddressLine { get; protected set; } = default!;
    public string Country { get; protected set; } = default!;
    public string State { get; protected set; } = default!;
    public string ZipCode { get; protected set; } = default!;
}