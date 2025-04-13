using Pdc.Domain.Models.Security;
using Pdc.Domain.Models.Versioning;

namespace Pdc.Domain.Models.Common;

public class RealisationContext : AChangeable
{
    internal void SetCreatedBy(User createdBy)
    {
        base.SetCreatedBy(createdBy);
    }
}
