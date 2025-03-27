using Pdc.Domain.Enums;

namespace Pdc.Domain.Entities.CourseFramework;

public class CourseFrameworkPerformanceCriteria
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required ContentElement ContentElement { get; set; }
    public required CourseFramework CourseFramework { get; set; }
    private IEnumerable<ContentElement> _contentElements { get; set; } = new List<ContentElement>();
    public required TeachedLevelType TeachedLevel { get; set; }
}
