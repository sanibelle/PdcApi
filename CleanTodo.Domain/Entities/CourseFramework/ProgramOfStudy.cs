namespace Pdc.Domain.Entities.CourseFramework;

public class ProgramOfStudy
{
    public Guid Id { get; set; }
    public string Code { get; set; } //420.B0
    public string Name { get; set; } //DEC, PRE-U
    public string Sanction { get; set; } //DEC, PRE-U
    public int MonthsDuration { get; set; } // 36 mois
    public int SpecificDurationHours { get; set; } // 36 mois
    public int TotalDurationHours { get; set; } // 36 mois
    public DateOnly IsCompleted { get; set; }
    public DateTime ApprovedDate { get; set; }
}
