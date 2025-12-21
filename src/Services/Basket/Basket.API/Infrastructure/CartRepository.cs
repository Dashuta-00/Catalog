using Basket.API.Exceptions;
using Basket.API.Models.ShoppingCart;
using Marten;

namespace Basket.API.Infrastructure;

public class CartRepository(IDocumentSession session) : ICartRepository
{
    public async Task<ShoppingCart?> GetCartAsync(string accountName, CancellationToken cancellationToken)
    {
        var cart = await session.LoadAsync<ShoppingCart>(accountName, cancellationToken);

        return cart ?? throw new UserNotFoundException(accountName);
    }

    public async Task<bool> RemoveCartAsync(string accountName, CancellationToken cancellationToken)
    {
        var existingCart = await session.LoadAsync<ShoppingCart>(accountName, cancellationToken);
         
        if (existingCart is null)
        {
            throw new CartNotFoundException(accountName);
        }
        
        session.Delete(existingCart);
        await session.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<ShoppingCart> SaveCartAsync(ShoppingCart shoppingCart, CancellationToken cancellationToken)
    {
        session.Store(shoppingCart);
        await session.SaveChangesAsync(cancellationToken);
        return shoppingCart;
    }
}