using Basket.API.Infrastructure;
using Basket.API.Models.ShoppingCart;
using Common.Kernel.CQRS.Commands;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.ShoppingBasket.Save;

public class SaveCartCommandHandler(ICartRepository cartRepository, IDistributedCache distributedCache) : ICommandHandler<SaveCartCommand, SaveCartResult>
{
    public async Task<SaveCartResult> Handle(SaveCartCommand command, CancellationToken cancellationToken)
    {
        var cart = new ShoppingCart
        {
            AccountName = command.AccountName,
            Items = command.Items
        };
        await cartRepository.SaveCartAsync(cart, cancellationToken);
        return new SaveCartResult(cart.AccountName);
    }
}