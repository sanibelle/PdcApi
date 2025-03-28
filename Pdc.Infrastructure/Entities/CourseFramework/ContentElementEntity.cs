using Pdc.Domain.Enums;
using Pdc.Infrastructure.Entities.Versioning;

namespace Pdc.Infrastructure.Entities.CourseFramework;

public class ContentElementEntity : ChangeableEntity
{
    public TeachedLevelType TeachedLevel { get; set; }
    public required CourseFrameworkPerformanceCriteriaEntity CourseFrameworkPerformanceCriteria { get; set; }
    public bool IsAssedElement { get; set; } = false;
}
