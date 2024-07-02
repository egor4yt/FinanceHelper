using FinanceHelper.Shared;
using FluentValidation;
using Microsoft.Extensions.Configuration;

namespace FinanceHelper.Application.Commands.Users.Register;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommandRequest>
{
    public RegisterUserCommandValidator(IConfiguration configuration)
    {
        RuleFor(v => v.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(v => v.Password)
            .NotEmpty();

        var supportedLocalizations = configuration.GetSection(ConfigurationKeys.SupportedLocalization).Get<string[]>()!;

        RuleFor(v => v.PreferredLocalizationCode)
            .Matches($"^({string.Join('|', supportedLocalizations)})$");
    }
}