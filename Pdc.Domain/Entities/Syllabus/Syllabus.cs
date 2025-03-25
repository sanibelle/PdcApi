using Pdc.Domain.Entities.Versioning;

namespace Pdc.Domain.Entities.Syllabus;

public class Syllabus
{
    public required Guid Id { get; set; }
    public required CourseFramework.CourseFramework MasterCoursePlan { get; set; }
    public required Versioning.ChangeRecord SyllabusVersion { get; set; }
    public required IList<Module> Modules { get; set; }
}
