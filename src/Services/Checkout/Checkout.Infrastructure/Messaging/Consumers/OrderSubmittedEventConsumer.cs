using Checkout.Application.Orders.Commands.ProcessOrderSubmission;
using Common.Messaging.Events;
using Mapster;
using MassTransit;
using MediatR;

namespace Checkout.Infrastructure.Messaging.Consumers;

public class OrderSubmittedEventConsumer(ISender sender)
    :IConsumer<OrderSubmittedEvent> 
{
    public async Task Consume(ConsumeContext<OrderSubmittedEvent> context)
    {
        var command = context
            .Message
            .Adapt<ProcessOrderSubmissionCommand>();
        
        await sender.Send(command);
    }
}