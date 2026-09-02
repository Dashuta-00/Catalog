using Checkout.Application.Orders.Commands.UpdateOrder.DTOs;
using Checkout.Domain.Orders;
using Checkout.Domain.Repositories;
using Common.Kernel.CQRS.Commands;
using Common.Kernel.Exceptions;
using Mapster;

namespace Checkout.Application.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand(string OrderId, UpdateOrderDto OrderData)
    : ICommand<UpdateOrderResult>;

public record UpdateOrderResult(bool IsSuccess);

public class UpdateOrderCommandHandler(IOrderRepository orderRepository)
: ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var id = Guid.Parse(command.OrderId);
        var existingOrder = await orderRepository.GetByIdAsync(id, cancellationToken);
        if (existingOrder is null)
        {
            throw new NotFoundException(nameof(Order), command.OrderId);
        }

        command.OrderData.Adapt(existingOrder);
        
        var isUpdated = await orderRepository.UpdateAsync(existingOrder, cancellationToken);
        return new UpdateOrderResult(isUpdated);
    }
}