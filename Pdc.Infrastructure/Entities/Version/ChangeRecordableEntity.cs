namespace Pdc.Infrastructure.Entities.Version;

public abstract class ChangeRecordableEntity
{
    public virtual ChangeRecordEntity? ChangeRecord { get; set; }
    public Guid? ChangeRecordId { get; set; }
}
