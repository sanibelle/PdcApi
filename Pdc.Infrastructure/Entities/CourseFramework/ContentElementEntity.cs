using Pdc.Domain.Enums;
using Pdc.Infrastructure.Entities.Versioning;
using Pdc.Infrastructure.Entities.Visitors;

namespace Pdc.Infrastructure.Entities.CourseFramework;

public class ContentElementEntity : ChangeableEntity
{
    public TeachedLevelType TeachedLevel { get; set; }
    public virtual CourseFrameworkPerformanceCriteriaEntity? CourseFrameworkPerformanceCriteria { get; set; }
    public bool IsAssedElement { get; set; } = false;
    public override T Accept<T>(IChangeableVisitor<T> visitor) => visitor.Visit(this);
}
