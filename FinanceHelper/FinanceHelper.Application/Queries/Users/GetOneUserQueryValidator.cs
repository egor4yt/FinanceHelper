using FluentValidation;

namespace FinanceHelper.Application.Queries.Users;

public class GetOneUserQueryValidator : AbstractValidator<GetOneUserQueryRequest>
{
    public GetOneUserQueryValidator()
    {
        RuleFor(v => v.Id)
            .GreaterThan(0);
    }
}