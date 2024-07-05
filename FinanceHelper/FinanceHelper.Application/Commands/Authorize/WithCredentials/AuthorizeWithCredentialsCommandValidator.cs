using FluentValidation;

namespace FinanceHelper.Application.Commands.Authorize.WithCredentials;

public class AuthorizeWithCredentialsCommandValidator : AbstractValidator<AuthorizeWithCredentialsCommandRequest>
{
    public AuthorizeWithCredentialsCommandValidator()
    {
        RuleFor(v => v.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(v => v.PasswordHash)
            .NotEmpty();
        
        RuleFor(x => x.JwtDescriptorDetails)
            .NotNull();
    }
}