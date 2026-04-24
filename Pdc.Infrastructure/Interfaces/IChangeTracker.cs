using Pdc.Infrastructure.Entities.Version;

namespace Pdc.Infrastructure.Interfaces;

internal interface IChangeTracker
{
    Task TrackAdd(ChangeableEntity entity, Guid changeRecordId);
    Task TrackUpdate(ChangeableEntity entity, string? oldValue, Guid changeRecordId);
    Task TrackDelete(ChangeableEntity entity, string? oldValue, Guid changeRecordId);
}
