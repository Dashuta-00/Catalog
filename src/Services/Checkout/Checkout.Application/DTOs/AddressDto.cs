namespace Checkout.Application.DTOs;

public record AddressDto(
    string Street,
    string City,
    string Region,
    string PostalCode);