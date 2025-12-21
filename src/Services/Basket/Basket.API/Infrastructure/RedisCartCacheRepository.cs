using System.Text.Json;
using Basket.API.Models.ShoppingCart;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Infrastructure;

public class RedisCartCacheRepository(
    ICartRepository cartRepository, 
    IDistributedCache distributedCache) : ICartRepository
{
    public async Task<ShoppingCart?> GetCartAsync(string accountName, CancellationToken cancellationToken)
    {
        var cached = await distributedCache.GetStringAsync(accountName, cancellationToken);
        if (!string.IsNullOrEmpty(cached))
        {
            return JsonSerializer.Deserialize<ShoppingCart>(cached);
        }
        
        var cart = await cartRepository.GetCartAsync(accountName, cancellationToken);
        await distributedCache.SetStringAsync(accountName, JsonSerializer.Serialize(cart), cancellationToken);
        return cart;
    }

    public async Task<bool> RemoveCartAsync(string accountName, CancellationToken cancellationToken)
    {
        var result = await cartRepository.RemoveCartAsync(accountName, cancellationToken);
        await distributedCache.RemoveAsync(accountName, cancellationToken);
        return result;
    }

    public async Task<ShoppingCart> SaveCartAsync(ShoppingCart shoppingCart, CancellationToken cancellationToken)
    {
        var result = await cartRepository.SaveCartAsync(shoppingCart, cancellationToken);
        await distributedCache.SetStringAsync(shoppingCart.AccountName, JsonSerializer.Serialize(shoppingCart), cancellationToken);
        return result;
    }
}