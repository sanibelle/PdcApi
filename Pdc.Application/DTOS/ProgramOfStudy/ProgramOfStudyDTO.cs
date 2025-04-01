using Pdc.Domain.Enums;
using Pdc.Domain.Models.Common;

namespace Pdc.Application.DTOS;

public class ProgramOfStudyDTO
{
    public required string Code { get; set; } //420.B0
    public required string Name { get; set; } //Techniques de l'informatique
    public required SanctionType Sanction { get; set; } //DEC, PRE-U
    public int MonthsDuration { get; set; } // 36 mois
    public int SpecificDurationHours { get; set; } // 2010
    public int TotalDurationHours { get; set; } // 5730
    public DateOnly PublishedOn { get; set; }
    /// <summary>
    /// Les unités obligatoires du programme
    /// </summary>
    public required Units SpecificUnits { get; set; }
    /// <summary>
    /// Les unités optionnelles du programmes
    /// </summary>
    public required Units OptionnalUnits { get; set; }
    /// <summary>
    /// Les unités des cours généraux
    /// </summary>
    public Units? GeneralUnits { get; set; }
    /// <summary>
    /// Les unités des cours complémentaires
    /// </summary>
    public Units? ComplementaryUnits { get; set; }
    public ICollection<CompetencyDTO> CompetencyDTOs { get; set; } = new List<CompetencyDTO>();

    public ProgramOfStudyDTO()
    {
    }
}
