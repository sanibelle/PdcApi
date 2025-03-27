using Pdc.Domain.Enums;
using Pdc.Infrastructure.Entities.MinisterialSpecification;
using Pdc.Infrastructure.Entities.Versioning;

namespace Pdc.Infrastructure.Entities.CourseFramework;

public class ContentElementEntity : ChangeableEntity
{
    public required PerformanceCriteriaEntity PerformanceCriteria { get; set; }
    public TeachedLevelType TeachedLevel { get; set; }
    public required CourseFrameworkPerformanceCriteriaEntity CourseFrameworkPerformanceCriteria { get; set; }

}
