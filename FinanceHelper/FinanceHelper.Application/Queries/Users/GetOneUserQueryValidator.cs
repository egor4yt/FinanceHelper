using FluentValidation;

namespace FinanceHelper.Application.Queries.Users;

public class GetOneUserQueryValidator : AbstractValidator<GetOneUserQueryRequest>
{
    public GetOneUserQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}