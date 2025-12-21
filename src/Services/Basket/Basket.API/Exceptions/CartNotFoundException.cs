using Common.Kernel.Exceptions;

namespace Basket.API.Exceptions;

public class CartNotFoundException : NotFoundException
{
    public CartNotFoundException(string accountName) 
        : base("ShoppingCart", accountName)
    {    }
}