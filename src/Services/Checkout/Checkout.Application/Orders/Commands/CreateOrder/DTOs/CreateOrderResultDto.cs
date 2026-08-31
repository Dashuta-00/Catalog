namespace Checkout.Application.Orders.Commands.CreateOrder.DTOs;

public record CreateOrderResultDto(
    Guid OrderId,
    string Message);