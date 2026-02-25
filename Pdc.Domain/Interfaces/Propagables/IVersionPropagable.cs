using Pdc.Domain.Models.Versioning;

namespace Pdc.Domain.Interfaces.Propagables;

public interface IVersionPropagable
{
    void SetVersionOnUntracked(ChangeRecord version);
}
