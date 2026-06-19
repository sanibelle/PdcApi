using Pdc.Domain.Models.Versioning;

namespace Pdc.Infrastructure.Interfaces;

internal interface IChangeApplier<T, TParent, TEntity>
{
    Task<TEntity> Add(ChangeRecord changeRecord, TParent parentEntity, T toAdd, IChangeTracker tracker);
    Task<TEntity> Update(ChangeRecord changeRecord, T toUpdate, IChangeTracker tracker);
    Task Delete(List<T> updated, ICollection<TEntity> existing, IChangeTracker tracker, Guid? changeRecordId = null);
}
