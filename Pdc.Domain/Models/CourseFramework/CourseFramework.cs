using Pdc.Domain.Interfaces.Propagables;
using Pdc.Domain.Interfaces.Versioning;
using Pdc.Domain.Models.Common;
using Pdc.Domain.Models.MinisterialSpecification;
using Pdc.Domain.Models.Security;
using Pdc.Domain.Models.Versioning;

namespace Pdc.Domain.Models.CourseFramework;

public class CourseFramework : IChangeRecordPropagable, ICreatedByPropagable, ICreatedOnPropagable, IChangeablesContainer
{
    public Guid? Id { get; set; }
    public string ProgramOfStudyCode { get; set; } = "";
    public IEnumerable<MinisterialCompetency> Competencies { get; set; } = [];
    public IEnumerable<CourseFrameworkCompetencyElement> CourseFrameworkPerformanceCriterias { get; set; } = [];
    public IEnumerable<CourseFramework> Prerequisites = [];
    public required Changeable Name { get; set; }
    public DateTime CreatedOn { get; set; }
    public required Changeable Code { get; set; }
    public required Weighting Weighting { get; set; }
    public required Changeable Semester { get; set; }
    /// <summary>
    /// Durée du cours en heures
    /// </summary>
    public int Hours { get; set; }
    public required Units Units { get; set; }
    public string FinalCourseObjective { get; set; } = string.Empty;
    public string CourseCharacteristics { get; set; } = string.Empty;
    /// <summary>
    /// Permet d'ajouter des sections supplémentaires propres aux différents départements sous la forme d'un texte
    /// </summary>
    public string? OtherSpecifications { get; set; }
    // TODO inférer le niveau atteint de taxonomie de Bloom
    public string StatementOfComplexAuthenticTask { get; set; } = string.Empty;
    public string TaskPresentation { get; set; } = string.Empty;
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
    ///        <definition><see cref="Changeable"/></definition>
    ///    </item>
    ///</list>
    /// </summary>
    public required IEnumerable<Changeable> AssedElements { get; set; } = [];
    public ChangeRecord? ChangeRecord { get; set; }

    public void SetChangeRecordOnUntracked(ChangeRecord changeRecord)
    {
        ChangeRecord = changeRecord;
    }

    public void SetCreatedByOnUntracked(User createdBy)
    {
        Name.SetCreatedByOnUntracked(createdBy);
        Code.SetCreatedByOnUntracked(createdBy);
        Weighting.SetCreatedByOnUntracked(createdBy);
        Semester.SetCreatedByOnUntracked(createdBy);
    }

    public void SetCreatedOnOnUntracked()
    {
        CreatedOn = DateTime.UtcNow;
        Name.SetCreatedOnOnUntracked();
        Code.SetCreatedOnOnUntracked();
        Weighting.SetCreatedOnOnUntracked();
        Semester.SetCreatedOnOnUntracked();
    }

    public void RemoveChangeablesByIds(List<Guid> changeableIdsToDelete)
    {
        throw new NotImplementedException();
    }

    public void SetValueById(Guid id, string value)
    {
        throw new NotImplementedException();
    }
}

