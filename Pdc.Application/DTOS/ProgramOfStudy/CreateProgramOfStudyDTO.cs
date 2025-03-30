using Pdc.Domain.Entities.Common;
using Pdc.Domain.Enums;

namespace Pdc.Application.DTOS;

public class CreateProgramOfStudyDTO
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

    public CreateProgramOfStudyDTO() { }
}
