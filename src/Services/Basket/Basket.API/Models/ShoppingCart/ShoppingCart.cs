namespace Basket.API.Models.ShoppingCart;

public class ShoppingCart
{
    public string AccountName { get; set; } = string.Empty;
    public List<ShoppingCartItem> Items { get; set; } = [];
    public decimal TotalPrice => Items.Sum(x => x.Quantity * x.UnitPrice);

    public ShoppingCart() { }

    public ShoppingCart(string accountName)
    {
        AccountName = accountName;
    }
}