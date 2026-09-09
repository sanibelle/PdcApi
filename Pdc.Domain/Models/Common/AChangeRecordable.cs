using Pdc.Domain.Interfaces.Propagables;
using Pdc.Domain.Models.Versioning;

namespace Pdc.Domain.Models.Common;

public abstract class AChangeRecordable : IChangeRecordPropagable
{
    public ChangeRecord? ChangeRecord { get; set; }

    public abstract void SetChangeRecordOnUntracked(ChangeRecord changeRecord);
    public bool IsDraftAndV1OrNull()
    {
        if (ChangeRecord == null)
        {
            return true;
        }
        return ChangeRecord.IsDraft && ChangeRecord.ChangeRecordNumber == 1;
    }

    public bool IsPublished()
    {
        if (ChangeRecord == null)
        {
            return false;
        }
        return !ChangeRecord.IsDraft;
    }

    public bool IsLatestVersion()
    {
        if (ChangeRecord == null)
        {
            return false;
        }
        return ChangeRecord.NextChangeRecord == null;
    }

}
