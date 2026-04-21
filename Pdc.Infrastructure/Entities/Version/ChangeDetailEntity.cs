using Pdc.Domain.Enums;

namespace Pdc.Infrastructure.Entities.Version;

public class ChangeDetailEntity
{
    public Guid? Id { get; set; }
    public required Guid ChangeRecordId { get; set; }
    public virtual ChangeRecordEntity? ChangeRecord { get; set; }
    public virtual ChangeableEntity? Changeable { get; set; }
    public required ChangeType ChangeType { get; set; }
    /// <summary>
    /// Holds the old value of the property when updated or deleted.
    /// </summary>
    public string? OldValue { get; set; }

}
