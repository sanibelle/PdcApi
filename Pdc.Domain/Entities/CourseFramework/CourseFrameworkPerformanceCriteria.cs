using Pdc.Domain.Entities.MinisterialSpecification;
using Pdc.Domain.Entities.Versioning;
using Pdc.Domain.Enums;

namespace Pdc.Domain.Entities.CourseFramework;

public class CourseFramerowkPerformanceCriteria : PerformanceCriteria
{
    public required ElementSpecification ElementSpecification { get; set; } // enseigné, vu plus tard, etc... 
    public required ChangeRecord MasterCoursePlanVersion { get; set; }
}
