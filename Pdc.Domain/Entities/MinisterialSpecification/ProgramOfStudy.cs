using Pdc.Domain.Entities.Common;
using Pdc.Domain.Entities.MinisterialSpecification;
using Pdc.Domain.Enums;

namespace Pdc.Domain.Entities.CourseFramework;

public class ProgramOfStudy // toujours issu d'un devis ministeriel
{
    /// <summary>
    /// Code unique de la formation
    /// </summary>
    public required string Code { get; set; } //420.B0
    /// <summary>
    /// Les unité spécifiques à la formation qui sont obligatoires
    /// </summary>
    public Units? SpecificUnits { get; set; }
    /// <summary>
    /// Les unités des programmes optionnels
    /// </summary>
    public Units? OptionnalUnits { get; set; }
    /// <summary>
    /// Les unités des cours généraux
    /// </summary>
    public Units GeneralUnits { get; set; } = new Units(16, 2, 3);
    /// <summary>
    /// Les unités des cours complémentaires
    /// </summary>
    public Units ComplementaryUnits { get; set; } = new Units(4);
    public required string Name { get; set; } //Techniques de l'informatique
    public required SanctionType Sanction { get; set; } //DEC, PRE-U
    public int MonthsDuration { get; set; } // 36 mois
    public int SpecificDurationHours { get; set; } // 2010
    public int TotalDurationHours { get; set; } // 5730
    public DateOnly PublishedOn { get; set; }
    public required IList<Competency> Competencies { get; set; }
}
