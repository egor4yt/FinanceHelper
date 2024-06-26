using FluentValidation;

namespace FinanceHelper.Application.Commands.Users.Register;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommandRequest>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(v => v.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(v => v.Password)
            .NotEmpty();
    }
}