using Pdc.Domain.Enums;
using Pdc.Infrastructure.Entities.MinisterialSpecification;

namespace Pdc.Infrastructure.Entities.CourseFramework;

public class CourseFrameworkPerformanceCriteriaEntity
{
    public Guid? Id { get; set; }
    public virtual PerformanceCriteriaEntity? PerformanceCriteria { get; set; }
    public virtual CourseFrameworkEntity? CourseFramework { get; set; }
    public virtual ICollection<ContentElementEntity>? ContentElements { get; set; }
    public required TeachedLevelType TeachedLevel { get; set; }
    public bool IsAssedElement { get; set; } = false;
}
