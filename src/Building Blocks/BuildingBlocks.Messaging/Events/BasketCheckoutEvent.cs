using System.Diagnostics.CodeAnalysis;

namespace BuildingBlocks.Messaging.Events;
public record BasketCheckoutEvent : IntegrationEvent
{
    public required string OrderName { get; set; }
    public required Guid CustomerId { get; set; }
    public required BasketCheckoutEventAddress BillingAddress { get; set; }
    public required BasketCheckoutEventAddress ShippingAddress { get; set; }
    public required BasketCheckoutEventPayment Payment { get; set; }
    public required IEnumerable<BasketCheckoutEventOrderItem> OrderItems { get; set; }
    public  decimal TotalPrice { get; set; }

    //public required OrderStatus Status { get; set; }
}

[method: SetsRequiredMembers]
public record BasketCheckoutEventOrderItem(Guid ProductId, decimal Price, int Quantity)
{
    public required Guid ProductId { get; set; } = ProductId;
    public required decimal Price { get; set; } = Price;
    public required int Quantity { get; set; } = Quantity;
}

public record BasketCheckoutEventPayment
{
    public BasketCheckoutEventPayment()
    {
        
    }

    [SetsRequiredMembers]
    public BasketCheckoutEventPayment(string cardName, string cardNumber, string expiration, string cVV, int paymentMethod)
    {
        CardName = cardName;
        CardNumber = cardNumber;
        Expiration = expiration;
        CVV = cVV;
        PaymentMethod = paymentMethod;
    }

    public required string CardName { get; set; }
    public required string CardNumber { get; set; }
    public required string Expiration { get; set; }
    public required string CVV { get; set; }
    public required int PaymentMethod { get; set; }
}

public record BasketCheckoutEventAddress
{
    [SetsRequiredMembers]
    public BasketCheckoutEventAddress(string firstName, string lastName, string? emailAddress, string addressLine, string country, string state, string zipCode)
    {
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        AddressLine = addressLine;
        Country = country;
        State = state;
        ZipCode = zipCode;
    }

    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? EmailAddress { get; set; } = default!;
    public required string AddressLine { get; set; }
    public required string Country { get; set; }
    public required string State { get; set; }
    public required string ZipCode { get; set; }
}