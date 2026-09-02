using Checkout.Domain.Enums;
using FluentValidation;

namespace Checkout.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty()
            .WithMessage("ID заказа обязателен");

        RuleFor(x => x.OrderData)
            .NotNull()
            .WithMessage("Данные для обновления обязательны");

        When(x => x.OrderData.ContactInfo is not null, () =>
        {
            RuleFor(x => x.OrderData.ContactInfo!.FirstName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.OrderData.ContactInfo!.LastName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.OrderData.ContactInfo!.Email)
                .NotEmpty()
                .EmailAddress();
        });

        When(x => x.OrderData.DeliveryAddress is not null, () =>
        {
            RuleFor(x => x.OrderData.DeliveryAddress!.Street)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.OrderData.DeliveryAddress!.City)
                .NotEmpty()
                .MaximumLength(100);
        });

        When(x => x.OrderData.PaymentMethod is PaymentMethod.CreditCard, () =>
        {
            RuleFor(x => x.OrderData.CardDetails)
                .NotNull()
                .WithMessage("Данные карты обязательны для CreditCard");
        });

        When(x => x.OrderData.PaymentMethod is PaymentMethod.BankTransfer, () =>
        {
            RuleFor(x => x.OrderData.CardDetails)
                .Null()
                .WithMessage("CardDetails должен быть null для BankTransfer");
        });
        
        
    }
}