using Checkout.Domain.Common;
using Checkout.Domain.Enums;
using Checkout.Domain.ValueObjects;

namespace Checkout.Domain.Orders;

public class Order : BaseEntity, IAggregateRoot
{
    public string AccountName { get; set; } = default!;
    public decimal TotalAmount { get; set; }
    public List<OrderItem> Items { get; set; } = [];

    public OrderStatus CurrentOrderStatus { get; set; } = OrderStatus.Draft;
    public Contact ContactInfo { get; set; } = default!;
    public Address DeliveryAddress { get; set; } = default!;
    public PaymentMethod CurrentPaymentMethod { get; set; }
    public CardDetails? CardDetails { get; set; }
    public PaymentStatus CurrentPaymentStatus { get; set; } = PaymentStatus.Pending;
}