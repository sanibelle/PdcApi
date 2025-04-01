using Pdc.Domain.Models.Versioning;
using Pdc.Domain.Enums;

namespace Pdc.Domain.Models.Common;

public class ContentElement : AChangeable
{
    public TeachedLevelType TeachedLevel { get; set; }

    public bool IsAssedElement { get; set; } = false;
}
