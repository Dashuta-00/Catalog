using Checkout.Application.DTOs;
using Checkout.Application.Orders.Commands.CreateOrder.DTOs;
using Checkout.Domain.Orders;
using Checkout.Domain.ValueObjects;
using Mapster;

namespace Checkout.Application.Mapping;

public class CreateOrderMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateOrderDto, Order>()
            .Map(o => o.CurrentPaymentMethod, src => src.PaymentMethod)
            .Map(O => O.CardDetails,
                src => src.CardDetails != null
                    ? src.CardDetails.Adapt<CardDetails>()
                    : null)
            .Map(o => o.TotalAmount, src => src.TotalPrice)
            .Map(o => o.ContactInfo, src => src.ContactInfo.Adapt<Contact>())
            .Map(o => o.DeliveryAddress, src => src.DeliveryAddress.Adapt<Address>())
            .Map(dest => dest.Items, src => src.Items.Select(item => new OrderItemDto(
                item.CatalogItemName,
                item.Quantity,
                item.UnitPrice)));

        config.NewConfig<OrderItemDto, OrderItem>();

        config.NewConfig<ContactDto, Contact>()
            .ConstructUsing(c => new Contact(
                c.FirstName,
                c.LastName,
                c.Email));
    }
}