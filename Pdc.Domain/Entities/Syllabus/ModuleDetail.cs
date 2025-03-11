using Pdc.Domain.Entities.CourseFramework;

namespace Pdc.Domain.Entities.Syllabus;

public class ModuleDetail
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required IList<CourseFrameworkContentElement> ContentElements { get; set; }
}
