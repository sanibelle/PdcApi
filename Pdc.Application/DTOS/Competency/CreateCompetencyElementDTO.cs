namespace Pdc.Application.DTOS.Common;

/// <summary>
/// Used for CompetencyElements and Performance criterias.
/// </summary>
public class CreateCompetencyElementDTO : CreateChangeableDTO
{
    public required IEnumerable<CreateChangeableDTO> PerformanceCriterias { get; set; }
}