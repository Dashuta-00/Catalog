namespace Checkout.Application.Orders.Commands.UpdateOrder.DTOs;

public record UpdateCardDetailsDto(
    string CardName, 
    string CardNumber,
    string Expiration,
    string Cvv);