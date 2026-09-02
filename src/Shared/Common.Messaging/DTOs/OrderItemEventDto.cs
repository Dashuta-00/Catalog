namespace Common.Messaging.DTOs;

public record OrderItemEventDto(
    string CatalofItemName,
    int Quantity,
    decimal UnitPrice);