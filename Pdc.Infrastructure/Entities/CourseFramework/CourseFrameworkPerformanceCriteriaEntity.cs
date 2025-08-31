using Pdc.Domain.Enums;
using Pdc.Infrastructure.Entities.MinisterialSpecification;

namespace Pdc.Infrastructure.Entities.CourseFramework;

public class CourseFrameworkPerformanceCriteriaEntity
{
    public required Guid? Id { get; set; }
    public required PerformanceCriteriaEntity PerformanceCriteria { get; set; }
    public required CourseFrameworkEntity CourseFramework { get; set; }
    public IEnumerable<ContentElementEntity> ContentElements { get; set; } = new List<ContentElementEntity>();
    public required TeachedLevelType TeachedLevel { get; set; }
    public bool IsAssedElement { get; set; } = false;
}
