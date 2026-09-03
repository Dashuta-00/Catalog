using Checkout.Application.Orders.Commands.CreateOrder.DTOs;
using Checkout.Domain.Enums;
using Checkout.Domain.Orders;
using Checkout.Domain.Repositories;
using Common.Kernel.CQRS.Commands;
using Mapster;

namespace Checkout.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler(IOrderRepository orderRepository)
    :ICommandHandler<CreateOrderCommand, CreateOrderResultDto>
{
    public async Task<CreateOrderResultDto> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = command.OrderData.Adapt<Order>();
        order.CurrentOrderStatus = OrderStatus.Draft;
        order.CurrentPaymentStatus = PaymentStatus.Pending;
        
        await orderRepository.AddAsync(order, cancellationToken);

        return new CreateOrderResultDto(order.Id, "Заказ создан");
    }
}