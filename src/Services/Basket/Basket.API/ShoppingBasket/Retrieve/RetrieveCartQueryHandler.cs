using Basket.API.Dto.RetrieveCart;
using Basket.API.Infrastructure;
using Common.Kernel.CQRS.Queries;

namespace Basket.API.ShoppingBasket.Retrieve;

public class RetrieveCartQueryHandler(ICartRepository cartRepository) 
    : IQueryHandler<RetrieveCartQuery, RetrieveCartResponse>
{
    public async Task<RetrieveCartResponse> Handle(RetrieveCartQuery query, CancellationToken cancellationToken)
    {
        var accountName = query.AccountName;
        var response = await cartRepository.GetCartAsync(accountName, cancellationToken);
        var result = new RetrieveCartResponse(response);
        return result;
    }
}