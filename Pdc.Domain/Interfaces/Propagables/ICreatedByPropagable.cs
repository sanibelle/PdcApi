using Pdc.Domain.Models.Security;

namespace Pdc.Domain.Interfaces.Propagables;

public interface ICreatedByPropagable
{
    void SetCreatedByOnUntracked(User createdBy);
}
