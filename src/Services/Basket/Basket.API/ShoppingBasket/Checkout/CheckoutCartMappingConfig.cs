using Basket.API.Models.ShoppingCart;
using Common.Messaging.DTOs;
using Common.Messaging.Events;
using Mapster;

namespace Basket.API.ShoppingBasket.Checkout;

public class CheckoutCartMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CheckoutCartRequest, CheckoutCartCommand>()
            .Map(dest => dest.CorrelationId, src => string.Empty);

        config.NewConfig<CheckoutCartResult, CheckoutCartResponse>()
            .Map(dest => dest.Message,
                src => "Заказ отправлен на обработку");
        
        //command -> event
        config.NewConfig<CheckoutCartCommand, OrderSubmittedEvent>()
            .Map(dest => dest.Items, src => new List<OrderItemEventDto>());

        config.NewConfig<ShoppingCartItem, OrderItemEventDto>()
            .Map(dest => dest.CatalofItemName,
                src => src.ItemTitle ?? "Unknown")
            .Map(dest => dest.Quantity, src => src.Quantity)
            .Map(dest => dest.UnitPrice, src => src.UnitPrice);
    }
}