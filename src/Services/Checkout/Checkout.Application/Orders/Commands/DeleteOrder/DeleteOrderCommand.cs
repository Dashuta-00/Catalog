using Checkout.Application.Orders.Commands.DeleteOrder.DTOs;
using Common.Kernel.CQRS.Commands;

namespace Checkout.Application.Orders.Commands.DeleteOrder;

public record DeleteOrderCommand(string OrderId) : ICommand<DeleteOrderResultDto>;