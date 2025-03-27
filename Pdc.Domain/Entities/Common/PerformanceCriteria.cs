using Pdc.Domain.Entities.Versioning;

namespace Pdc.Domain.Entities.MinisterialSpecification;

public class PerformanceCriteria : AChangeable
{
    public required int Position { get; set; }
}
