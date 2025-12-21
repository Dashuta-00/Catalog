using Basket.API.Models.ShoppingCart;

namespace Basket.API.Dto.SaveCart;

public record SaveCartRequest(string AccountName, List<ShoppingCartItem> Items);