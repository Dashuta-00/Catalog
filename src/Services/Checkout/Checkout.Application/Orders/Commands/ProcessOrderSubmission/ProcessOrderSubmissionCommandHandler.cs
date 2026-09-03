using Checkout.Domain.Orders;
using Checkout.Domain.Repositories;
using Common.Kernel.CQRS.Commands;
using Mapster;

namespace Checkout.Application.Orders.Commands.ProcessOrderSubmission;

public class ProcessOrderSubmissionCommandHandler(IOrderRepository orderRepository)
    : ICommandHandler<ProcessOrderSubmissionCommand, ProcessOrderSubmissionResult>
{
    public async Task<ProcessOrderSubmissionResult> Handle(ProcessOrderSubmissionCommand command, CancellationToken cancellationToken)
    {
        var existingOrder = await orderRepository.GetByIdAsync(command.OrderId, cancellationToken);

        if (existingOrder is not null)
        {
            return new ProcessOrderSubmissionResult(existingOrder.Id);
        }

        var order = command.Adapt<Order>();
        await orderRepository.AddAsync(order, cancellationToken);
        return new ProcessOrderSubmissionResult(order.Id);
    }
}