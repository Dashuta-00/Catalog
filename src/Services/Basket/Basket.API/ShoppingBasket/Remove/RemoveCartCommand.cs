using Common.Kernel.CQRS.Commands;

namespace Basket.API.ShoppingBasket.Remove;

public record RemoveCartCommand(string AccountName) : ICommand<RemoveCartResult>;