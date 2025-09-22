using Pdc.Infrastructure.Entities.Identity;

namespace Pdc.Infrastructure.Entities.Versioning;

public class ChangeableEntity
{
    public Guid? Id { get; set; }
    public required string Value { get; set; }
    public required ICollection<ComplementaryInformationEntity> ComplementaryInformations { get; set; }
    internal virtual void SetCreatedBy(IdentityUserEntity createdBy)
    {
        ComplementaryInformations.ToList().ForEach(x => x.SetCreatedBy(createdBy));
    }
}
