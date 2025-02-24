using Pdc.Domain.Entities.Common;
using Pdc.Domain.Entities.Version;
using Pdc.Domain.Enums;

namespace Pdc.Domain.Entities.CourseFramework;

public class CourseFrameworkContentElement : ContentElement
{
    // Name Diagrammes UML
    public required ElementSpecification ElementSpecification { get; set; } // enseigné, vu plus tard, etc... 
    public required MasterCoursePlanVersion MasterCoursePlanVersion { get; set; }
}
