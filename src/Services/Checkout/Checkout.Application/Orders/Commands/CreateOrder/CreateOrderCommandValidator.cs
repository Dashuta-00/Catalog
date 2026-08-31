using Checkout.Domain.Enums;
using FluentValidation;

namespace Checkout.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandValidator
    : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.OrderData).NotNull();
        RuleFor(x => x.OrderData.AccountName).NotEmpty().Length(3, 20);

        RuleFor(x => x.OrderData.Items).NotEmpty();
        RuleForEach(x => x.OrderData.Items).ChildRules(i =>
        {
            i.RuleFor(x => x.Quantity).GreaterThan(0);
            i.RuleFor(x => x.UnitPrice).GreaterThan(0);
        });
        
        RuleFor(x => x.OrderData)
            .Must(d => Math.Abs(d.TotalPrice - d.Items.Sum(
                i => i.Quantity * i.UnitPrice)) < 0.01m)
            .WithMessage("Сумма не совпадает");

        When(x => x.OrderData.PaymentMethod == PaymentMethod.CreditCard, () =>
        {
            RuleFor(x => x.OrderData.CardDetails)
                .NotNull()
                .WithMessage("Детали карты обязательны при CreditCard");
            RuleFor(x => x.OrderData.CardDetails!.CardNumber)
                .CreditCard()
                .WithMessage("Неверный номер карты");
            RuleFor(x => x.OrderData.CardDetails!.Expiration)
                .NotEmpty()
                .WithMessage("Срок действия обязателен");
            RuleFor(x => x.OrderData.CardDetails!.Cvv)
                .Matches(@"\d{3,4}$")
                .WithMessage("CVV должен быть 3-4 цифры");
        });

        When(x => x.OrderData.PaymentMethod != PaymentMethod.CreditCard, () =>
        {
            RuleFor(x => x.OrderData.CardDetails)
                .Null()
                .WithMessage("CardDetails должен быть null для BankTransfer");
        });
    }
}