using Basket.API.Exceptions;
using Basket.API.ShoppingBasket.Remove;
using Basket.API.ShoppingBasket.Retrieve;
using Common.Kernel.CQRS.Commands;
using Common.Messaging.DTOs;
using Common.Messaging.Events;
using Mapster;
using MassTransit;
using MediatR;

namespace Basket.API.ShoppingBasket.Checkout;

public class CheckoutCartCommandHandler(
    IPublishEndpoint publishEndpoint,
    ISender sender) : ICommandHandler<CheckoutCartCommand, CheckoutCartResult>
{
    public async Task<CheckoutCartResult> Handle(CheckoutCartCommand command, CancellationToken cancellationToken)
    {
        var accountName = command.AccountName;
        var query = new RetrieveCartQuery(accountName);
        var basket = await sender.Send(query, cancellationToken);

        if (basket?.Cart is null)
        {
            throw new CartNotFoundException(accountName);
        }

        var orderId = Guid.NewGuid();
        var orderEvent = command.Adapt<OrderSubmittedEvent>();
        orderEvent.OrderId = orderId;
        orderEvent.TotalPrice = basket.Cart.TotalPrice;

        orderEvent.Items = basket.Cart.Items
            .Select(item => item.Adapt<OrderItemEventDto>())
            .ToList();

        await publishEndpoint.Publish(orderEvent, cancellationToken);

        var removeCart = new RemoveCartCommand(accountName);
        var result = await sender.Send(removeCart, cancellationToken);

        return new CheckoutCartResult(
            orderId,
            command.CorrelationId,
            result.IsSuccess);
    }
}