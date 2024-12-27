namespace Basket.API.DTOs;
public record PaymentDto : BasketCheckoutEventPayment
{
    [SetsRequiredMembers]
    public PaymentDto(string cardName, string cardNumber, string expiration, string cVV, int paymentMethod) : base(cardName, cardNumber, expiration, cVV, paymentMethod)
    {
    }
}