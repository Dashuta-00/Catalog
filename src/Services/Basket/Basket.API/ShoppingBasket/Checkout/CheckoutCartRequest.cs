namespace Basket.API.ShoppingBasket.Checkout;

public record CheckoutCartRequest(
    string AccountName,
    string FirstName,
    string LastName,
    string Email,
    string Street,
    string City,
    string Region,
    string PostalCode,
    int PaymentMethod,
    string? CardName,
    string? CardNumber,
    string? Expiration,
    string? Cvv
);