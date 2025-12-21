using Basket.API.Dto.RetrieveCart;
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
        var result = new RetrieveCartResult
        (
            new RetrieveCartResponse(
            Items: response.Items,
            TotalPrice: response.TotalPrice
        ));
        return result;
    }
}