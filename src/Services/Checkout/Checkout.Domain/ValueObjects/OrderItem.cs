namespace Checkout.Domain.ValueObjects;

public record OrderItem(
    string CatalogItemName,
    int Quantity,
    decimal UnitPrice);