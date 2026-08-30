namespace Checkout.Application.DTOs;

public record OrderItemDto(
    string CatalogItemName,
    int Quantity,
    decimal UnitPrice);