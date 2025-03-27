namespace Pdc.Infrastructure.Entities.Versioning;

public abstract class VersionableEntity
{
    public ChangeRecordEntity CurrentVersion { get; set; } = new ChangeRecordEntity();
}
