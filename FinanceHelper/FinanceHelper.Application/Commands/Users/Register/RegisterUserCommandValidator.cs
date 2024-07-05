using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace FinanceHelper.Application.Commands.Users.Register;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommandRequest>
{
    public RegisterUserCommandValidator(IOptions<RequestLocalizationOptions> localizationOptions)
    {
        var supportedLocalizations = localizationOptions.Value.SupportedCultures!.Select(x => x.TwoLetterISOLanguageName).ToList();

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.PasswordHash)
            .NotEmpty();

        RuleFor(x => x.PreferredLocalizationCode)
            .Matches($"^({string.Join('|', supportedLocalizations)})$");

        RuleFor(x => x.JwtDescriptorDetails)
            .NotNull();
    }
}