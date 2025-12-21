using Basket.API.Models.ShoppingCart;

namespace Basket.API.Infrastructure;

public interface ICartRepository
{
    Task<ShoppingCart?> GetCartAsync(string accountName, CancellationToken cancellationToken);
    Task<bool> RemoveCartAsync(string accountName, CancellationToken cancellationToken);
    Task<ShoppingCart> SaveCartAsync(ShoppingCart shoppingCart, CancellationToken cancellationToken);
}