using Pdc.Infrastructure.Entities.Version;
using Pdc.Infrastructure.Interfaces;

namespace Pdc.Infrastructure.ChangeTracker;

internal class NoOpChangeDetailsTracker : IChangeTracker
{
    public Task TrackAdd(ChangeableEntity entity, Guid changeRecordId) => Task.CompletedTask;
    public Task TrackUpdate(ChangeableEntity entity, string? oldValue, Guid changeRecordId) => Task.CompletedTask;
    public Task TrackDelete(ChangeableEntity entity, string? oldValue, Guid changeRecordId) => Task.CompletedTask;
}
