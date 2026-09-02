using FluentValidation;

namespace Checkout.Application.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandValidator
    : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator()
    {
        RuleFor(o => o.OrderId)
            .NotEmpty()
            .WithMessage("OrderId заказа не должен быть пустым")
            .Must(id => Guid.TryParse(id, out _))
            .WithMessage("OrderId заказа должен быть корректным Guid");
    }
}