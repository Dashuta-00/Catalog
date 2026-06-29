using Basket.API.Dto.RetrieveCart;
using Common.Kernel.CQRS.Queries;

namespace Basket.API.ShoppingBasket.Retrieve;

public record RetrieveCartQuery(string AccountName)
    : IQuery<RetrieveCartResponse>;