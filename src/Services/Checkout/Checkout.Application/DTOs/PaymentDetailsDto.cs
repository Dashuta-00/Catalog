namespace Checkout.Application.DTOs;

public record PaymentDetailsDto(
    string CardName,
    string MaskedCardNumber,
    string Expiration);