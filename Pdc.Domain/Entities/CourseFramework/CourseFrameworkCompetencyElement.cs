using Pdc.Domain.Entities.Common;
using Pdc.Domain.Entities.Version;
using Pdc.Domain.Enums;

namespace Pdc.Domain.Entities.CourseFramework;

public class CourseFrameworkCompetencyElement : CompetencyElement
{
    public required ElementSpecification ElementSpecification { get; set; } // enseigné, vu plus tard, etc... 
    public required MasterCoursePlanVersion MasterCoursePlanVersion { get; set; }
}
