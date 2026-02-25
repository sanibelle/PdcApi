using Pdc.Domain.Models.Common;
using Pdc.Domain.Models.MinisterialSpecification;
using Pdc.Domain.Models.Versioning;

namespace Pdc.Domain.Models.CourseFramework;

public class CourseFramework
{
    public IEnumerable<MinisterialCompetency> Competencies { get; set; } = new List<MinisterialCompetency>();
    public IEnumerable<CourseFrameworkCompetencyElement> CourseFrameworkPerformanceCriterias { get; set; } = new List<CourseFrameworkCompetencyElement>();
    public IEnumerable<CourseFramework> Prerequisites = new List<CourseFramework>();
    public required string Name { get; set; }
    public required string CourseCode { get; set; }
    public required Weighting Weighting { get; set; }
    public required int Semester { get; set; }
    /// <summary>
    /// Durée du cours en heures
    /// </summary>
    public required int Hours { get; set; }
    public required Units Units { get; set; }
    public required string FinalCourseObjective { get; set; }
    public required string CourseCharacteristics { get; set; }
    /// <summary>
    /// Permet d'ajouter des sections supplémentaires propres aux différents départements sous la forme d'un texte
    /// </summary>
    public string? OtherSpecifications { get; set; }
    // TODO inférer le niveau atteint de taxonomie de Bloom
    public required string StatementOfComplexAuthenticTask { get; set; }
    public required string TaskPresentation { get; set; }
    // RealisationContexts est imposé par le devis ministériel
    /// <summary>
    /// Éléments évalués. Peut être un : 
    /// <list type="bullet">
    ///    <item>
    ///        <term>Un élément de compétence</term>
    ///        <definition><see cref="MinisterialCompetencyElement"/></definition>
    ///    </item>
    ///    <item>
    ///        <term>Un critère de performance</term>
    ///        <definition><see cref="PerformanceCriteria"/></definition>
    ///    </item>
    ///    <item>
    ///        <term>Un élément de contenu</term>
    ///        <definition><see cref="ContentElement"/></definition>
    ///    </item>
    ///    <item>
    ///        <term>Du texte</term>
    ///        <definition><see cref="AChangeable"/></definition>
    ///    </item>
    ///</list>
    /// </summary>
    public required IEnumerable<AChangeable> AssedElements { get; set; }
}

