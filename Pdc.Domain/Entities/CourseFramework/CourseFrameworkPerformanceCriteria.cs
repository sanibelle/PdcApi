using Pdc.Domain.Entities.Common;
using Pdc.Domain.Entities.MinisterialSpecification;
using Pdc.Domain.Enums;

namespace Pdc.Domain.Entities.CourseFramework;

public class CourseFrameworkPerformanceCriteria : PerformanceCriteria
{
    public IEnumerable<ContentElement> ContentElements { get; set; } = new List<ContentElement>();
    public required TeachedLevelType TeachedLevel { get; set; }
    public bool IsAssedElement { get; set; } = false;
}
