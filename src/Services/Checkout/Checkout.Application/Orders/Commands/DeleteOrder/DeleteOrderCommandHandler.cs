using Checkout.Application.Orders.Commands.DeleteOrder.DTOs;
using Checkout.Domain.Repositories;
using Common.Kernel.CQRS.Commands;
using FluentValidation;

namespace Checkout.Application.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandHandler(IOrderRepository orderRepository)
    : ICommandHandler<DeleteOrderCommand, DeleteOrderResultDto>
{
    public async Task<DeleteOrderResultDto> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        var orderIdGuid = Guid.Parse(command.OrderId); 
        var order = await orderRepository.GetByIdAsync(orderIdGuid, cancellationToken);

        if (order is null)
        {
            throw new ValidationException($"Заказ с OrderId {orderIdGuid} не найден");
        }
        
        var result = await orderRepository.DeleteOrderById(orderIdGuid, cancellationToken);
        return new DeleteOrderResultDto(result);
    }
}