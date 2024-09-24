using FinanceHelper.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.Queries.SupportedLanguages;

public class GetLocalizedSupportedLanguagesQueryHandler(ApplicationDbContext applicationDbContext) : IRequestHandler<GetLocalizedSupportedLanguagesQueryRequest, GetLocalizedSupportedLanguagesQueryResponse>
{
    public async Task<GetLocalizedSupportedLanguagesQueryResponse> Handle(GetLocalizedSupportedLanguagesQueryRequest request, CancellationToken cancellationToken)
    {
        var response = new GetLocalizedSupportedLanguagesQueryResponse();
        response.Items = [];

        response.Items = await applicationDbContext.SupportedLanguages
            .Select(x => new GetLocalizedSupportedLanguagesQueryResponseItem
            {
                Code = x.Code,
                Value = x.LocalizedValue
            })
            .ToListAsync(cancellationToken);
        
        return response;
    }
}