namespace Checkout.Application.DTOs;

public record ContactDto(
    string FirstName,
    string LastName,
    string Email);