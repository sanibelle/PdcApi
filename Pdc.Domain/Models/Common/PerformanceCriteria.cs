using Pdc.Domain.Models.Versioning;

namespace Pdc.Domain.Models.MinisterialSpecification;

public class PerformanceCriteriaDTO : AChangeable
{
    public required int Position { get; set; }
}
