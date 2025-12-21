using Common.Kernel.Exceptions;

namespace Basket.API.Exceptions;

public class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(string accountName) 
        : base("AccountName not found", accountName)
    {    }
}