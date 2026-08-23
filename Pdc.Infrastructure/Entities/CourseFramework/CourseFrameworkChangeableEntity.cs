using Pdc.Infrastructure.Entities.Version;
using Pdc.Infrastructure.Entities.Visitors;

namespace Pdc.Infrastructure.Entities.CourseFramework;

public class CourseFrameworkChangeableEntity : ChangeableEntity
{
    public virtual CourseFrameworkEntity? CourseFramework { get; set; }
    public override T Accept<T>(IChangeableVisitor<T> visitor) => visitor.Visit(this);
}
