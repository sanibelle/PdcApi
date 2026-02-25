namespace Pdc.Infrastructure.Entities.Versioning;

public abstract class VersionableEntity
{
    public virtual required ChangeRecordEntity CurrentVersion { get; set; }
}
