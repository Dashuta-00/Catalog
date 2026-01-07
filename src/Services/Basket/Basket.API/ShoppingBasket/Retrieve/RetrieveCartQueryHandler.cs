using Basket.API.Infrastructure;
using Common.Kernel.CQRS.Queries;

namespace Basket.API.ShoppingBasket.Retrieve;

public class RetrieveCartQueryHandler(ICartRepository cartRepository) 
    : IQueryHandler<RetrieveCartQuery, RetrieveCartResult>
{
    public async Task<RetrieveCartResult> Handle(RetrieveCartQuery query, CancellationToken cancellationToken)
    {
        var accountName = query.AccountName;
        var response = await cartRepository.GetCartAsync(accountName, cancellationToken);
        var result = new RetrieveCartResult(response);
        return result;
    }
}