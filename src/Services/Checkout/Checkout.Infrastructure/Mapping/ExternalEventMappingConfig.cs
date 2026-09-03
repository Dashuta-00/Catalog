using Checkout.Application.DTOs;
using Checkout.Application.Orders.Commands.ProcessOrderSubmission;
using Common.Messaging.DTOs;
using Common.Messaging.Events;
using Mapster;

namespace Checkout.Infrastructure.Mapping;

public class ExternalEventMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<
            OrderSubmittedEvent,
            ProcessOrderSubmissionCommand>();
        config.NewConfig<OrderItemEventDto, OrderItemDto>();
    }
}