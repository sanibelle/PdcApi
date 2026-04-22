using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pdc.Domain.Enums;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Entities.Version;

namespace Pdc.Infrastructure.Repositories;

public class ChangeDetailsRepository(AppDbContext context, IMapper mapper) : IChangeDetailsRepository
{
    public async Task<List<Guid>> FindDeletedChangeableIdByChangeRecordId(Guid changeRecordId)
    {
        return await FindChangeableIdByChangeRecordId(changeRecordId, ChangeType.Delete);
    }

    /// <summary>
    /// Returns all the changeable ids of the given type recursively from the changeRecordId to the first one.
    /// </summary>
    /// <param name="changeRecordId"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    private async Task<List<Guid>> FindChangeableIdByChangeRecordId(Guid changeRecordId, ChangeType type)
    {
        ChangeRecordEntity? changeRecord = await context.ChangeRecords
            .Where(x => x.Id == changeRecordId)
            .SingleOrDefaultAsync();

        List<Guid> ids = await context.ChangeDetails
            .Where(x => x.ChangeRecordId == changeRecordId)
            .Where(x => x.ChangeType == type)
            .Select(x => x.Changeable.Id!.Value)
            .ToListAsync();

        if (changeRecord.ParentChangeRecordId.HasValue)
        {
            var parentIds = await FindChangeableIdByChangeRecordId(changeRecord.ParentChangeRecordId.Value, type);
            return [.. parentIds, .. ids];
        }
        return ids;
    }
}
