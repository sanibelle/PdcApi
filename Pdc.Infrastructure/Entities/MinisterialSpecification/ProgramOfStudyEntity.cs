using Pdc.Domain.Enums;
using Pdc.Infrastructure.Entities.MinisterialSpecification;

namespace Pdc.Infrastructure.Entities.CourseFramework;

public class ProgramOfStudyEntity // toujours issu d'un devis ministeriel
{
    /// <summary>
    /// Code unique de la formation
    /// </summary>
    public required string Code { get; set; } //420.B0
    /// <summary>
    /// Les unité spécifiques à la formation qui sont obligatoires
    /// </summary>
    public virtual UnitsEntity? SpecificUnits { get; set; }
    /// <summary>
    /// Les unités des programmes optionnels
    /// </summary>
    public virtual UnitsEntity? OptionalUnits { get; set; }
    /// <summary>
    /// Les unités des cours généraux
    /// </summary>
    public virtual UnitsEntity? GeneralUnits { get; set; }
    /// <summary>
    /// Les unités des cours complémentaires
    /// </summary>
    public virtual UnitsEntity? ComplementaryUnits { get; set; }
    public required string Name { get; set; } //Techniques de l'informatique
    public required ProgramType ProgramType { get; set; } //DEC, PRE-U
    public int MonthsDuration { get; set; } // 36 mois
    public int SpecificDurationHours { get; set; } // 2010
    public int TotalDurationHours { get; set; } // 5730
    public DateOnly PublishedOn { get; set; }
    public virtual ICollection<CompetencyEntity>? Competencies { get; set; }
}
