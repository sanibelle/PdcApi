namespace Pdc.Infrastructure.Entities.Versioning;

public abstract class VersionableEntity
{
    public required ChangeRecordEntity CurrentVersion { get; set; }
}
