using Basket.API.ShoppingBasket.Save;
using FluentValidation;

namespace Basket.API.Validation;

public class SaveCartCommandValidator : AbstractValidator<SaveCartCommand>
{
    public SaveCartCommandValidator()
    {
        RuleFor(x => x.Items)
            .NotNull()
            .WithMessage("Корзина не может быть null");

        RuleFor(x => x.AccountName)
            .NotEmpty()
            .WithMessage("AccountName обязателен")
            .MaximumLength(100)
            .WithMessage("AccountName слишком длинный");
        
        RuleFor(x => x.Items)
            .NotEmpty()
            .WithMessage("Корзина не может быть пустой");

        RuleForEach(x => x.Items).ChildRules(item =>
        {
            item.RuleFor(i => i.ItemId)
                .NotEqual(Guid.Empty)
                .WithMessage("ItemId должен быть валидным Guid");
            
            item.RuleFor(i => i.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity должен быть больше 0");
            
            item.RuleFor(i => i.UnitPrice)
                .GreaterThan(0)
                .WithMessage("UnitPrice должен быть больше 0");
            
            item.RuleFor(i => i.ItemTitle)
                .NotEmpty()
                .WithMessage("ItemTitle не должен быть пустым");
        });
    }
}