using Checkout.Domain.Orders;
using Checkout.Domain.Repositories;
using Common.Kernel.CQRS.Queries;

namespace Checkout.Application.Orders.Queries.GetOrdersByAccountName;

public record GetOrdersByAccountNameQuery(string AccountName)
    : IQuery<GetOrdersByAccountNameResult>;

public record GetOrdersByAccountNameResult(IEnumerable<Order> Orders);

public class GetOrdersByAccountNameQueryHandler(IOrderRepository orderRepository)
    :IQueryHandler<GetOrdersByAccountNameQuery, GetOrdersByAccountNameResult>
{
    public async Task<GetOrdersByAccountNameResult> Handle(GetOrdersByAccountNameQuery query, CancellationToken cancellationToken)
    {
        var orders = await orderRepository.GetOrdersByAccountName(query.AccountName);

        return new GetOrdersByAccountNameResult(orders);
    }
}