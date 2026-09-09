using Pdc.Domain.DTOS.Common;

namespace Pdc.Application.DTOS.Common;

/// <summary>
/// Used for CompetencyElements and Performance criterias.
/// </summary>
public class CompetencyElementDTO : ChangeableDTO<string>
{
    public required ICollection<ChangeableDTO<string>> PerformanceCriterias { get; set; }
}