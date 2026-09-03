using Checkout.Application.Orders.Commands.UpdateOrder.DTOs;
using Common.Kernel.CQRS.Commands;

namespace Checkout.Application.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand(string OrderId, UpdateOrderDto OrderData)
    : ICommand<UpdateOrderResult>;