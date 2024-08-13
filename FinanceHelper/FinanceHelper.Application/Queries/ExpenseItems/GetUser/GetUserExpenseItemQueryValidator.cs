using FluentValidation;

namespace FinanceHelper.Application.Queries.ExpenseItems.GetUser;

public class GetUserExpenseItemQueryValidator : AbstractValidator<GetUserExpenseItemQueryRequest>
{
    public GetUserExpenseItemQueryValidator()
    {
        RuleFor(x => x.OwnerId)
            .GreaterThan(0);
    }
}