using Pdc.Domain.Entities.Common;
using Pdc.Domain.Enums;

namespace Pdc.Domain.Entities.Version;

public class Change
{
    public required Guid Id { get; set; }
    public Changeable? From { get; set; }
    public Changeable? To { get; set; }
    public ChangeAction ChangeAction
    {
        get
        {
            if (From == null) return ChangeAction.Added;
            if (To == null) return ChangeAction.Deleted;
            return ChangeAction.Modified;
        }
    }

    public Change(Changeable? from, Changeable? to)
    {
        Id=Guid.NewGuid();
        if (from == null && to == null) throw new ArgumentException("Both from and to cannot be null");
        From = from;
        To = to;
    }
}
