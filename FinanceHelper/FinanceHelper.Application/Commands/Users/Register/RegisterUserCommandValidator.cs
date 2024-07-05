using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace FinanceHelper.Application.Commands.Users.Register;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommandRequest>
{
    public RegisterUserCommandValidator(IOptions<RequestLocalizationOptions> localizationOptions)
    {
        RuleFor(v => v.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(v => v.PasswordHash)
            .NotEmpty();

        var supportedLocalizations = localizationOptions.Value.SupportedCultures!.Select(x => x.TwoLetterISOLanguageName).ToList();

        RuleFor(v => v.PreferredLocalizationCode)
            .Matches($"^({string.Join('|', supportedLocalizations)})$");
    }
}