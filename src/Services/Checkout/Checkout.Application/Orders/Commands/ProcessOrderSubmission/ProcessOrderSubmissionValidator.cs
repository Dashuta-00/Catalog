using FluentValidation;

namespace Checkout.Application.Orders.Commands.ProcessOrderSubmission;

public class ProcessOrderSubmissionValidator : AbstractValidator<ProcessOrderSubmissionCommand>
{
    public ProcessOrderSubmissionValidator()
    {
        RuleFor(o => o.OrderId)
            .NotEmpty()
            .WithMessage("OrderId обязателен");
        
        RuleFor(o => o.AccountName)
            .NotEmpty()
            .Length(3, 20)
            .WithMessage("AccountName должен быть от 3 до 20 символов");
        
        RuleFor(o => o.TotalPrice)
            .GreaterThan(0)
            .WithMessage("TotalPrice должен быть больше 0");
        
        RuleFor(o => o.Items)
            .NotEmpty()
            .WithMessage("Items не должен быть пустым");

        RuleForEach(x => x.Items)
            .ChildRules(item =>
            {
                item.RuleFor(x => x.Quantity).GreaterThan(0);
                item.RuleFor(x => x.UnitPrice).GreaterThan(0);
            });
    }
}