using Pdc.Domain.Entities.MinisterialSpecification;
using Pdc.Domain.Enums;

namespace Pdc.Domain.Entities.CourseFramework;

/// <summary>
/// La Value perment d'y inscrire le texte en lien avec le cours.
/// </summary>
public class CourseFrameworkCompetencyElement
{
    public required CompetencyElement CompetencyElement { get; set; }
    public required CourseFramework CourseFramework { get; set; }
    public required int Hours { get; set; }
    public bool IsTerminalyEvaluated
    {
        get
        {
            return ReachedLevel == BloomTaxonomy.Creating;
        }
    }
    public BloomTaxonomy ReachedLevel { get; set; }
    public bool IsTeached { get; set; } = true;
    public TeachedLevelType TeachedLevel { get; set; }
}
