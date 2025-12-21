using Basket.API.Models;
using Basket.API.Models.ShoppingCart;
using Common.Kernel.CQRS.Commands;

namespace Basket.API.ShoppingBasket.Save;

public record SaveCartCommand(string AccountName, List<ShoppingCartItem> Items) : ICommand<SaveCartResult>;