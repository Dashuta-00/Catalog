namespace Checkout.Application.Orders.Commands.UpdateOrder.DTOs;

public record UpdateOrderRequestDto(
    string OrderId,
    UpdateOrderDto OrderData);