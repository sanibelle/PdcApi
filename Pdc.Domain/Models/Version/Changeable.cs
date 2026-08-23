using Pdc.Domain.Interfaces.Propagables;
using Pdc.Domain.Models.Security;
using System.Globalization;

namespace Pdc.Domain.Models.Versioning;

public class Changeable : IChangeRecordPropagable, ICreatedByPropagable, ICreatedOnPropagable
{
    public Guid? Id { get; set; }
    public string Value { get; set; }
    public List<ComplementaryInformation> ComplementaryInformations { get; set; } = [];

    public Changeable()
    {
        Value = string.Empty;
    }
    public Changeable(string value)
    {
        Value = value;
    }

    public Changeable(int value)
    {
        Value = value.ToString();
    }

    public Changeable(double value)
    {
        Value = value.ToString(CultureInfo.InvariantCulture); // . instead of , for decimal separator
    }


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
