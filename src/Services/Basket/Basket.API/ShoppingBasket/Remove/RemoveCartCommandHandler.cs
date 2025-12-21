using Basket.API.Infrastructure;
using Common.Kernel.CQRS.Commands;
using FluentValidation;

namespace Basket.API.ShoppingBasket.Remove;

public class RemoveCartCommandHandler(ICartRepository cartRepository, IValidator<RemoveCartCommand> validator) : ICommandHandler<RemoveCartCommand, RemoveCartResult>
{
    public async Task<RemoveCartResult> Handle(RemoveCartCommand command, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(command, cancellationToken);
        var result = await cartRepository.RemoveCartAsync(command.AccountName, cancellationToken);
        return new RemoveCartResult(result);
    }
}