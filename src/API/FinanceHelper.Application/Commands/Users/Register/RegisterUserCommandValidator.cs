using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace FinanceHelper.Application.Commands.Users.Register;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommandRequest>
{
    public RegisterUserCommandValidator(IOptions<RequestLocalizationOptions> localizationOptions)
    {
        var supportedLocalizations = localizationOptions.Value.SupportedCultures!.Select(x => x.TwoLetterISOLanguageName).ToList();

        When(x => x.TelegramChatId.HasValue == false, () => FromApiValidation(supportedLocalizations))
            .Otherwise(FromTelegramValidation);
    }

    private void FromApiValidation(IEnumerable<string> supportedLocalizations)
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.PasswordHash)
            .NotEmpty();

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(32)
            .MinimumLength(2);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(32)
            .MinimumLength(2);

        RuleFor(x => x.PreferredLocalizationCode)
            .Matches($"^({string.Join('|', supportedLocalizations)})$");

        RuleFor(x => x.JwtDescriptorDetails)
            .NotNull();
    }

    private void FromTelegramValidation()
    {
        RuleFor(x => x.TelegramChatId)
            .NotNull()
            .GreaterThan(0);
    }
}