using Pdc.Domain.Entities.MinisterialSpecification;

namespace Pdc.Domain.Entities.CourseFramework;

public class CourseFramework
{
    public required IList<CourseFrameworkCompetencyElement> PerformanceCriterias { get; set; }
    public required MinisterialCompetency MinisterialCompetency { get; set; }
}
