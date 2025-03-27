using Pdc.Domain.Enums;

namespace Pdc.Infrastructure.Entities.CourseFramework;

public class CourseFrameworkPerformanceCriteriaEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required ContentElementEntity ContentElement { get; set; }
    public required CourseFrameworkEntity CourseFramework { get; set; }
    private IEnumerable<ContentElementEntity> _contentElements { get; set; } = new List<ContentElementEntity>();
    public required TeachedLevelType TeachedLevel { get; set; }
    public bool IsAssedElement { get; set; } = false;
}
