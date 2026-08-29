namespace Checkout.Domain.ValueObjects;

public record CardDetails(
    string CardName,
    string CardNumber,
    string Expiration,
    string Cvv);