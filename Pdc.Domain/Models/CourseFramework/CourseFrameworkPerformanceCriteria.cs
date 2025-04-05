using Pdc.Domain.Models.Common;
using Pdc.Domain.Models.MinisterialSpecification;
using Pdc.Domain.Enums;

namespace Pdc.Domain.Models.CourseFramework;

public class CourseFrameworkPerformanceCriteria : PerformanceCriteriaDTO
{
    public IEnumerable<ContentElement> ContentElements { get; set; } = new List<ContentElement>();
    public required TeachedLevelType TeachedLevel { get; set; }
    public bool IsAssedElement { get; set; } = false;
}
