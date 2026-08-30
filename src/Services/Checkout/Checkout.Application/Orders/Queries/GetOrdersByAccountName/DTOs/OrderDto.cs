using Checkout.Application.DTOs;

namespace Checkout.Application.Orders.Queries.GetOrdersByAccountName.DTOs;

public record OrderDto(
    Guid Id,
    string AccountName,
    decimal TotalPrice,
    string OrderStatus,
    ContactDto ContactInfo,
    AddressDto DeliveryAddress,
    IEnumerable<OrderItemDto> Items,
    string PaymentMethod,
    PaymentDetailsDto? PaymentDetails,
    string PaymentStatus,
    DateTime CreatedAt);