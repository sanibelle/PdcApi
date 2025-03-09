using Pdc.Domain.Entities.MinisterialSpecification;
using Pdc.Domain.Enums;

namespace Pdc.Application.DTOS;

public class ProgramOfStudyDTO
{
    public Guid Id { get; set; }
    public required string Code { get; set; } //420.B0
    public required string Name { get; set; } //Techniques de l'informatique
    public required SanctionType Sanction { get; set; } //DEC, PRE-U
    public double MonthsDuration { get; set; } // 36 mois
    public int SpecificDurationHours { get; set; } // 2010
    public int TotalDurationHours { get; set; } // 5730
    public DateOnly PublishedOn { get; set; }
    public required IList<MinisterialCompetency> Competencies { get; set; }

    public ProgramOfStudyDTO() { }

}
