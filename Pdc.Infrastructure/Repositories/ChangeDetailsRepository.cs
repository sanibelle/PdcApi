using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pdc.Domain.Enums;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.Versioning;
using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Entities.Version;

namespace Pdc.Infrastructure.Repositories;

public class ChangeDetailsRepository(AppDbContext context, IMapper mapper) : IChangeDetailsRepository
{
    public async Task<List<Guid>> FindDeletedChangeableIdByChangeRecordId(Guid changeRecordId, int? recordNumberToSkip = null)
    {
        return await FindChangeableIdByChangeRecordId(changeRecordId, ChangeType.Delete, recordNumberToSkip);
    }

    public async Task<List<ChangeDetail>> GetChangeDetailsByChangeRecordId(Guid changeRecordId)
    {
        List<ChangeDetailEntity> changeDetailEntities = await context.ChangeDetails
            .Where(x => x.ChangeRecordId == changeRecordId)
            .Include(x => x.Changeable)
            .ToListAsync();

        return mapper.Map<List<ChangeDetail>>(changeDetailEntities);
    }

    /// <summary>
    /// Returns all the changeable ids of the given type recursively from the changeRecordId to the first one.
    /// </summary>
    /// <param name="changeRecordId">the change record id that will initiate the research</param>
    /// <param name="type">The type to look for</param>
    /// <param name="changeRecordToSkip">the change record id to skip in the research</param>
    /// <returns></returns>
    private async Task<List<Guid>> FindChangeableIdByChangeRecordId(Guid changeRecordId, ChangeType type, int? recordNumberToSkip = null)
    {
        ChangeRecordEntity? changeRecord = await context.ChangeRecords
            .Where(x => x.Id == changeRecordId)
            .SingleOrDefaultAsync();
        List<Guid> ids = [];

        if (!recordNumberToSkip.HasValue || changeRecord.ChangeRecordNumber != recordNumberToSkip.Value)
        {
            ids = await context.ChangeDetails
                .Where(x => x.ChangeRecordId == changeRecordId)
                .Where(x => x.ChangeType == type)
                .Select(x => x.Changeable.Id!.Value)
                .ToListAsync();
        }

        if (changeRecord.ParentChangeRecordId.HasValue)
        {
            var parentIds = await FindChangeableIdByChangeRecordId(changeRecord.ParentChangeRecordId.Value, type, recordNumberToSkip);
            return [.. parentIds, .. ids];
        }
        return ids;
    }
}
