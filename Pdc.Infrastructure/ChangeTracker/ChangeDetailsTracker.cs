using Pdc.Domain.Enums;
using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Entities.Version;
using Pdc.Infrastructure.Interfaces;

namespace Pdc.Infrastructure.ChangeTracker;

internal class ChangeDetailsTracker(AppDbContext context) : IChangeTracker
{
    public async Task TrackAdd(ChangeableEntity entity, Guid changeRecordId)
    {
        await Create(entity, null, ChangeType.Add, changeRecordId);
    }

    public async Task TrackUpdate(ChangeableEntity entity, string? oldValue, Guid changeRecordId)
    {
        if (entity.Value != oldValue)
        {
            await Create(entity, oldValue, ChangeType.Update, changeRecordId);
        }
    }

    public async Task TrackDelete(ChangeableEntity entity, string? oldValue, Guid changeRecordId)
    {
        await Create(entity, oldValue, ChangeType.Delete, changeRecordId);
    }

    private async Task Create(ChangeableEntity entity, string? oldValue, ChangeType changeType, Guid changeRecordId)
    {
        await context.ChangeDetails.AddAsync(new ChangeDetailEntity
        {
            ChangeRecordId = changeRecordId,
            ChangeType = changeType,
            Changeable = entity,
            OldValue = oldValue
        });
    }
}