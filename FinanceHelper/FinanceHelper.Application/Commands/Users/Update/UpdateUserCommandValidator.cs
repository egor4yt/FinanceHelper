using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace FinanceHelper.Application.Commands.Users.Update;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommandRequest>
{
    public UpdateUserCommandValidator(IOptions<RequestLocalizationOptions> localizationOptions)
    {
        var supportedLocalizations = localizationOptions.Value.SupportedCultures!.Select(x => x.TwoLetterISOLanguageName).ToList();

        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.PreferredLocalizationCode)
            .Matches($"^({string.Join('|', supportedLocalizations)})$");

        RuleFor(x => x.JwtDescriptorDetails)
            .NotNull();
    }
}