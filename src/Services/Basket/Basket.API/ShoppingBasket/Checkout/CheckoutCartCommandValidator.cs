using FluentValidation;

namespace Basket.API.ShoppingBasket.Checkout;

public class CheckoutCartCommandValidator : AbstractValidator<CheckoutCartCommand>
{
    public CheckoutCartCommandValidator()
    {
        RuleFor(x => x.AccountName)
            .NotEmpty()
            .Length(3, 20)
            .WithMessage("Account должен быть от 3 до 20 символов");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Имя обязательно и не должно превышать 100 символов");
        
        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Фамилия обязательна и не должна превышать 100 символов");

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Некорректный email адрес");

        RuleFor(x => x.Street)
            .NotEmpty()
            .MaximumLength(200)
            .WithMessage("Адрес обязателен");

        RuleFor(x => x.City)
            .NotEmpty()
            .MaximumLength(200)
            .WithMessage("Город обязателен");

        When(x => x.PaymentMethod == 0, () => //Credit card
        {
            RuleFor(x => x.CardName)
                .NotEmpty()
                .WithMessage("Имя держателя карты обязательно");

            RuleFor(x => x.CardNumber)
                .NotEmpty()
                .CreditCard()
                .WithMessage("Некорректный номер карты");

            RuleFor(x => x.Expiration)
                .NotEmpty()
                .WithMessage("Срок действия карты обязателен");

            RuleFor(x => x.Cvv)
                .NotEmpty()
                .Matches(@"^\d{3,4}$")
                .WithMessage("CVV должен содержать 3-4 цифры");
        });

        When(x => x.PaymentMethod == 1, () => // BankTransfer
        {
            RuleFor(x => x.CardName).Null();
            RuleFor(x => x.CardNumber).Null();
            RuleFor(x => x.Expiration).Null();
            RuleFor(x => x.Cvv).Null();
        });
    }
}