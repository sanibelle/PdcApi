using Pdc.Infrastructure.Entities.Identity;

namespace Pdc.Infrastructure.Entities.Versioning;

public abstract class VersionableEntity
{
    public required ChangeRecordEntity CurrentVersion { get; set; }

    internal virtual void SetCreatedBy(IdentityUserEntity createdBy)
    {
        CurrentVersion.SetCreatedBy(createdBy);
    }
}
