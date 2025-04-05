namespace Pdc.Application.DTOS.Common;

/// <summary>
/// Used for CompetencyElements and Performance criterias.
/// </summary>
public class CompetencyElementDTO : ChangeableDTO
{
    public required ICollection<ChangeableDTO> PerformanceCriterias { get; set; }
}