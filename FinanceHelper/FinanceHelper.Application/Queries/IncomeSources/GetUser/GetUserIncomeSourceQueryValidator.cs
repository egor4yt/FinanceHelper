using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace FinanceHelper.Application.Queries.IncomeSources.GetUser;

public class GetUserIncomeSourceQueryValidator : AbstractValidator<GetUserIncomeSourceQueryRequest>
{
    public GetUserIncomeSourceQueryValidator(IOptions<RequestLocalizationOptions> localizationOptions)
    {
        var supportedLocalizations = localizationOptions.Value.SupportedCultures!.Select(x => x.TwoLetterISOLanguageName).ToList();
        
        RuleFor(x => x.OwnerId)
            .GreaterThan(0);
        
        RuleFor(x => x.LocalizationCode)
            .Matches($"^({string.Join('|', supportedLocalizations)})$");
    }
}