using Pdc.Domain.Entities.MinisterialSpecification;
using Pdc.Domain.Enums;

namespace Pdc.Domain.Entities.CourseFramework;

/// <summary>
/// La Value perment d'y inscrire le texte en lien avec le cours.
/// </summary>
public class CourseFrameworkCompetencyElement : CompetencyElement
{
    public IEnumerable<CourseFrameworkPerformanceCriteria> PerformanceCriterias { get; set; } = new List<CourseFrameworkPerformanceCriteria>();

    public bool IsTerminalyEvaluated
    {
        get
        {
            return ReachedTaxonomyLevel == BloomTaxonomy.Creating;
        }
    }
    public BloomTaxonomy ReachedTaxonomyLevel { get; set; }
    public TeachedLevelType TeachedLevel { get; set; }
    public bool IsAssedElement { get; set; } = false;
}
