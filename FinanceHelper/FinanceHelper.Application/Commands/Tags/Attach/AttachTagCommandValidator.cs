using FluentValidation;

namespace FinanceHelper.Application.Commands.Tags.Attach;

public class AttachTagCommandValidator : AbstractValidator<AttachTagCommandRequest>
{
    public AttachTagCommandValidator()
    {
        RuleFor(x => x.EntityId)
            .GreaterThan(0);

        RuleFor(x => x.TagId)
            .GreaterThan(0);

        RuleFor(x => x.OwnerId)
            .GreaterThan(0);

        RuleFor(x => x.TagTypeCode)
            .NotEmpty();
    }
}