﻿namespace Domain.ValueObjects;
public record Payment
{
    public string CardName { get; private set; } = default!;
    public string CardNumber { get; private set; } = default!;
    public string Expiration { get; private set; } = default!;
    public string CVV { get; private set; } = default!;
    public int PaymentMethod { get; private set; } = default!;

    private Payment(string cardName, string cardNumber, string expiration, string cvv, int paymentMethod)
    {
        CardName = cardName;
        CardNumber = cardNumber;
        Expiration = expiration;
        CVV = cvv;
        PaymentMethod = paymentMethod;
    }

    public static Payment Of(string cardName, string cardNumber, string expiration, string cvv, int paymentMethod)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(cardName);
        ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber);
        ArgumentException.ThrowIfNullOrWhiteSpace(cvv);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length, 3);

        return new Payment(cardName, cardNumber,expiration, cvv, paymentMethod);
    }
}
