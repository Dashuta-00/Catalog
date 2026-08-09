using Basket.API.Infrastructure;
using Basket.API.Models.ShoppingCart;
using Common.Kernel.CQRS.Commands;
using Promotion.Grpc.Protos;

namespace Basket.API.ShoppingBasket.Save;

public class SaveCartCommandHandler(ICartRepository cartRepository, PromoService.PromoServiceClient promoService) : ICommandHandler<SaveCartCommand, SaveCartResult>
{
    public async Task<SaveCartResult> Handle(SaveCartCommand command, CancellationToken cancellationToken)
    {
        var cart = new ShoppingCart
        {
            AccountName = command.AccountName,
            Items = command.Items
        };
        
        await ApplyPromotionsToCartAsync(cart, cancellationToken);       
        await cartRepository.SaveCartAsync(cart, cancellationToken);
        
        return new SaveCartResult(cart.AccountName);
    }

    private async Task ApplyPromotionsToCartAsync(ShoppingCart cart, CancellationToken cancellationToken)
    {
        foreach (var item in cart.Items)
        {
            var discount = await GetDiscountForItemAsync(cancellationToken, item);
            if (discount is null) continue;

            item.UnitPrice = ApplyDiscount(item.UnitPrice, (decimal)discount.Value);
        }
    }

    private static decimal ApplyDiscount(decimal unitPrice, decimal discount)
    {
        var newPrice = unitPrice - discount;
        return newPrice < 0 ? 0m : newPrice;
    }

    private async Task<PromoModel?> GetDiscountForItemAsync(CancellationToken cancellationToken, ShoppingCartItem item)
    {
        var getPromoRequest = new GetPromoRequest
        {
            CatalogItemId = item.ItemId.ToString()
        };

        var discount = await promoService.GetPromoAsync(getPromoRequest, cancellationToken: cancellationToken);
        return discount;
    }
}