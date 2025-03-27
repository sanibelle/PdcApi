using Pdc.Domain.Entities.Versioning;
using Pdc.Domain.Enums;

namespace Pdc.Domain.Entities.Common;

public class ContentElement : AChangeable
{
    public TeachedLevelType TeachedLevel { get; set; }

    public bool IsAssedElement { get; set; } = false;
}
