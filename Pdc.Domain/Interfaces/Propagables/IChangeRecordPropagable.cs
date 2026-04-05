using Pdc.Domain.Models.Versioning;

namespace Pdc.Domain.Interfaces.Propagables;

public interface IChangeRecordPropagable
{
    void SetChangeRecordOnUntracked(ChangeRecord changeRecord);
}
