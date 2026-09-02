using Checkout.Application.DTOs;
using Checkout.Domain.Enums;

namespace Checkout.Application.Orders.Commands.UpdateOrder.DTOs;

public record UpdateOrderDto(
    ContactDto? ContactInfo,
    AddressDto? DeliveryAddress,
    PaymentMethod? PaymentMethod,
    UpdateCardDetailsDto? CardDetails,
    OrderStatus? Status);