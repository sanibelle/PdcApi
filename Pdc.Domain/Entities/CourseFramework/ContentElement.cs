using Pdc.Domain.Entities.MinisterialSpecification;
using Pdc.Domain.Entities.Versioning;

namespace Pdc.Domain.Entities.CourseFramework;

public class ContentElement : Changeable
{
    public required PerformanceCriteria PerformanceCriteria { get; set; }

}
