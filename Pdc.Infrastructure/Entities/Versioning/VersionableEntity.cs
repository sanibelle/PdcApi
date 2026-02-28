namespace Pdc.Infrastructure.Entities.Versioning;

public abstract class VersionableEntity
{
    public virtual ChangeRecordEntity? CurrentVersion { get; set; }
}
