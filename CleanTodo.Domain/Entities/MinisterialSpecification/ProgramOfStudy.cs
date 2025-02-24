using Pdc.Domain.Entities.MinisterialSpecification;

namespace Pdc.Domain.Entities.CourseFramework;

public class ProgramOfStudy // toujours issu d'un devis ministeriel
{
    public Guid Id { get; set; }
    public required string Code { get; set; } //420.B0
    public required string Name { get; set; } //Techniques de l'informatique
    public required string Sanction { get; set; } //DEC, PRE-U
    public int MonthsDuration { get; set; } // 36 mois
    public int SpecificDurationHours { get; set; } // 2010
    public int TotalDurationHours { get; set; } // 5730
    public DateOnly IsCompleted { get; set; }
    public DateTime ApprovedDate { get; set; }
    public required IList<MinisterialCompetency> Competencies { get; set; }
}
