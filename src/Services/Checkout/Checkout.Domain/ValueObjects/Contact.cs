namespace Checkout.Domain.ValueObjects;

public record Contact(
    string FirstName,
    string LastName,
    string Email);