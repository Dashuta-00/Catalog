namespace Basket.API.ShoppingBasket.Checkout;

public record CheckoutCartResponse(
    Guid OrderId,
    string CorrelationId,
    string? Message
);