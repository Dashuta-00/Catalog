namespace Basket.API.ShoppingBasket.Checkout;

public record CheckoutCartResult(
    Guid OrderId,
    string CorrelationId,
    bool CartRemoved);