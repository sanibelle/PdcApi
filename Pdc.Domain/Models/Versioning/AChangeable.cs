using Pdc.Domain.Interfaces.Propagables;
using Pdc.Domain.Models.Security;

namespace Pdc.Domain.Models.Versioning;

public abstract class AChangeable : IVersionPropagable, ICreatedByPropagable
{
    public required Guid? Id { get; set; }
    public required string Value { get; set; }
    public required List<ComplementaryInformation> ComplementaryInformations { get; set; } = [];

    public virtual void SetVersionOnUntracked(ChangeRecord version)
    {
        if (version is null) throw new ArgumentNullException(nameof(version));
        ComplementaryInformations.ForEach(x => x.SetVersionOnUntracked(version));
    }

    public virtual void SetCreatedByOnUntracked(User user)
    {
        ComplementaryInformations.ForEach(x => x.SetCreatedByOnUntracked(user));
    }
}
