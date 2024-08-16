using FinanceHelper.Application.Exceptions;
using FinanceHelper.Application.Services.Interfaces;
using FinanceHelper.Domain.Entities;
using FinanceHelper.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceHelper.Application.Commands.Tags.Attach;

public class AttachTagCommandHandler(ApplicationDbContext applicationDbContext, IStringLocalizer<AttachTagCommandHandler> stringLocalizer) : IRequestHandler<AttachTagCommandRequest>
{
    public async Task Handle(AttachTagCommandRequest request, CancellationToken cancellationToken)
    {
        var existsTag = await applicationDbContext.Tags
            .AnyAsync(x => x.Id == request.TagId
                           && x.TagTypeCode == request.TagTypeCode
                           && x.OwnerId == request.OwnerId, cancellationToken);
        if (existsTag == false) throw new BadRequestException(stringLocalizer["DoesNotExistsTag"]);


        var existsEntity = await TaggableEntityExistsAsync(request.EntityId, request.OwnerId, request.TagTypeCode, cancellationToken);
        if (existsEntity == false) throw new BadRequestException(stringLocalizer["DoesNotExistsEntity"]);

        var existsTagMap = await applicationDbContext.TagsMap
            .AnyAsync(x => x.TagId == request.TagId
                           && x.EntityId == request.EntityId, cancellationToken);
        if (existsTagMap) throw new BadRequestException(stringLocalizer["AlreadyExistsTagMap"]);

        var map = new TagMap
        {
            TagId = request.TagId,
            EntityId = request.EntityId
        };

        await applicationDbContext.TagsMap.AddAsync(map, cancellationToken);
        await applicationDbContext.SaveChangesAsync(cancellationToken);
    }

    private Task<bool> TaggableEntityExistsAsync(long entityId, long ownerId, string tagType, CancellationToken cancellationToken = default)
    {
        if (tagType == Domain.Metadata.TagType.ExpenseItem.Code) return applicationDbContext.ExpenseItems.AnyAsync(x => x.Id == entityId && x.OwnerId == ownerId, cancellationToken);
        if (tagType == Domain.Metadata.TagType.IncomeSource.Code) return applicationDbContext.IncomeSources.AnyAsync(x => x.Id == entityId && x.OwnerId == ownerId, cancellationToken);

        throw new BadRequestException(stringLocalizer["DoesNotExistsTagType"]);
    }
}