using FluentValidation;

namespace Checkout.Application.Orders.Queries.GetOrdersByAccountName;

public class GetOrdersByAccountNameValidator
    : AbstractValidator<GetOrdersByAccountNameQuery>
{
    public GetOrdersByAccountNameValidator()
    {
        RuleFor(x => x.AccountName)
            .NotEmpty()
            .WithMessage("Имя аккаунта обязательно для заполнения")
            .NotNull()
            .WithMessage("Имя аккаунта не может быть пустым")
            .MinimumLength(3)
            .WithMessage("Имя аккаунта должно содержать минимум 3 символа")
            .MaximumLength(20)
            .WithMessage("Имя аккаунта не может превышать 20 символов")
            .Matches("^[a-zA-Z0-9_@]+$")
            .WithMessage("Имя аккаунта может содержать буквы, цифры, подчеркивание, @, точку и дефис");
    }
}