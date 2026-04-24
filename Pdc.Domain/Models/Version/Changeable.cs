using Pdc.Domain.Interfaces.Propagables;
using Pdc.Domain.Models.Security;

namespace Pdc.Domain.Models.Versioning;

public class Changeable : IChangeRecordPropagable, ICreatedByPropagable, ICreatedOnPropagable
{
    public required Guid? Id { get; set; }
    public required string Value { get; set; }
    public required List<ComplementaryInformation> ComplementaryInformations { get; set; } = [];

    public virtual void SetChangeRecordOnUntracked(ChangeRecord changeRecord)
    {
        if (changeRecord is null) throw new ArgumentNullException(nameof(changeRecord));
        ComplementaryInformations.ForEach(x => x.SetChangeRecordOnUntracked(changeRecord));
    }

    public virtual void SetCreatedByOnUntracked(User user)
    {
        ComplementaryInformations.ForEach(x => x.SetCreatedByOnUntracked(user));
    }

    public virtual void SetCreatedOnOnUntracked()
    {
        ComplementaryInformations.ForEach(x => x.SetCreatedOnOnUntracked());
    }
}
