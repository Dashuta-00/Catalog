namespace Checkout.Application.Orders.Commands.CreateOrder.DTOs;

public record CreateCardDetailsDto(
    string CardName, 
    string CardNumber,
    string Expiration,
    string Cvv);