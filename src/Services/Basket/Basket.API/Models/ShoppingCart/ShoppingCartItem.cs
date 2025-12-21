namespace Basket.API.Models.ShoppingCart;

public class ShoppingCartItem
{
    public Guid ItemId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public string? ItemTitle { get; set; }
    public string? ItemNote { get; set; }
}