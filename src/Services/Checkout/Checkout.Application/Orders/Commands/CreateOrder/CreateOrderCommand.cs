using Checkout.Application.Orders.Commands.CreateOrder.DTOs;
using Common.Kernel.CQRS.Commands;

namespace Checkout.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(CreateOrderDto OrderData)
    : ICommand<CreateOrderResultDto>;