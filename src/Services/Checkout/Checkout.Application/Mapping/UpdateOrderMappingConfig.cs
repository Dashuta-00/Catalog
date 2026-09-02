using Checkout.Application.Orders.Commands.UpdateOrder.DTOs;
using Checkout.Domain.Orders;
using Mapster;

namespace Checkout.Application.Mapping;

public class UpdateOrderMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UpdateOrderDto, Order>()
            .IgnoreNullValues(true)
            .Map(dest => dest.ContactInfo, src => src.ContactInfo)
            .Map(dest => dest.DeliveryAddress, src => src.DeliveryAddress)
            .Map(dest => dest.CurrentPaymentMethod, src => src.PaymentMethod)
            .Map(dest => dest.CardDetails, src => src.CardDetails)
            .Map(dest => dest.CurrentOrderStatus, src => src.Status);
    }
}