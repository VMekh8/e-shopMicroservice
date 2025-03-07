﻿namespace Ordering.Domain.ValueObjects;

public record Payment
{
    public string? CardName { get; } = string.Empty;

    public string CardNumber { get; } = string.Empty;

    public string Expiration { get; } = string.Empty;

    public string CVV { get; } = string.Empty;

    public int PaymentMethod { get; } = 0;

    private Payment(
        string? cardName,
        string cardNumber,
        string expiration,
        string cvv,
        int paymentMethod)
    {
        CardName = cardName;
        CardNumber = cardNumber;
        Expiration = expiration;
        CVV = cvv;
        PaymentMethod = paymentMethod;
    }

    private Payment()
    {
    }

    public static Payment Of(
        string? cardName,
        string cardNumber,
        string expiration,
        string cvv,
        int paymentMethod)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(cardName);
        ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber);
        ArgumentException.ThrowIfNullOrWhiteSpace(cvv);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length, 3);

        return new Payment(cardName, cardNumber, expiration, cvv, paymentMethod);
    }
}