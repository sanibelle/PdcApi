using Pdc.Domain.Entities.MinisterialSpecification;
using Pdc.Domain.Entities.Versioning;
using Pdc.Domain.Enums;

namespace Pdc.Domain.Entities.CourseFramework;

public class ContentElement : Changeable
{
    public required PerformanceCriteria PerformanceCriteria { get; set; }
    public TeachedLevelType TeachedLevel { get; set; }
    public required CourseFrameworkPerformanceCriteria CourseFrameworkPerformanceCriteria { get; set; }

}
