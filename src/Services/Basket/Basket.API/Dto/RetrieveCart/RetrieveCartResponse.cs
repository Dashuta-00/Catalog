using Basket.API.Models.ShoppingCart;

namespace Basket.API.Dto.RetrieveCart;

public record RetrieveCartResponse(
    List<ShoppingCartItem> Items,
    decimal TotalPrice);