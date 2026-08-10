using Basket.API.Infrastructure;
using Basket.API.Models.ShoppingCart;
using Common.Kernel.CQRS.Commands;
using Grpc.Core;
using Promotion.Grpc.Protos;

namespace Basket.API.ShoppingBasket.Save;

public class SaveCartCommandHandler(ICartRepository cartRepository, PromoService.PromoServiceClient promoService, ILogger<SaveCartCommandHandler> logger) 
    : ICommandHandler<SaveCartCommand, SaveCartResult>
{
    public async Task<SaveCartResult> Handle(SaveCartCommand command, CancellationToken cancellationToken)
    {
        var existingCart = await cartRepository.GetCartAsync(command.AccountName, cancellationToken);
        
        var cart = new ShoppingCart
        {
            AccountName = command.AccountName,
            Items = command.Items
        };
        
        await ApplyPromotionsToCartAsync(cart, existingCart, cancellationToken);       
        await cartRepository.SaveCartAsync(cart, cancellationToken);
        
        return new SaveCartResult(cart.AccountName);
    }

    private async Task ApplyPromotionsToCartAsync(ShoppingCart cart, ShoppingCart? existingCart, CancellationToken cancellationToken)
    {
        foreach (var item in cart.Items)
        {
            var existingItem = existingCart?.Items.FirstOrDefault(i => i.ItemId == item.ItemId);

            if (existingItem is not null && item.UnitPrice == existingItem.UnitPrice)
            {
                continue;
            }

            try
            {
                var discount = await GetDiscountForItemAsync(item, cancellationToken);
                if (discount is null) continue;

                item.UnitPrice = ApplyDiscount(item.UnitPrice, (decimal)discount.Value);
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.DeadlineExceeded)
            {
                logger.LogWarning(ex, "Таймаут при получении скидки для товара {ItemId}", item.ItemId);
            }
            catch (RpcException ex)
            {
                logger.LogError(ex, "Ошибка при получении скидки для товара {ItemId}: {Message}", item.ItemId, ex.Message);
            }
        }
    }

    private static decimal ApplyDiscount(decimal unitPrice, decimal discount)
    {
        var newPrice = unitPrice - discount;
        return newPrice < 0 ? 0m : newPrice;
    }

    private async Task<PromoModel?> GetDiscountForItemAsync(ShoppingCartItem item, CancellationToken cancellationToken)
    {
        var getPromoRequest = new GetPromoRequest
        {
            CatalogItemId = item.ItemId.ToString()
        };
        
        var discount = await promoService.GetPromoAsync(
            getPromoRequest, 
            deadline: DateTime.UtcNow.AddSeconds(2), 
            cancellationToken: cancellationToken);
        return discount;
    }
}