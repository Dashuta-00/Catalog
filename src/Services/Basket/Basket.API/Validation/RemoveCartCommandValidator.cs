using Basket.API.ShoppingBasket.Remove;
using FluentValidation;

namespace Basket.API.Validation;

public class RemoveCartCommandValidator : AbstractValidator<RemoveCartCommand>
{
    public RemoveCartCommandValidator()
    {
        RuleFor(x => x.AccountName)
            .NotEmpty()
            .WithMessage("Account name is required");
    }
    
}