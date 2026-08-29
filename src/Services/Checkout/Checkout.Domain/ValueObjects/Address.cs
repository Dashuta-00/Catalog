namespace Checkout.Domain.ValueObjects;

public record Address(
    string Street,
    string City,
    string Region,
    string PostalCode);