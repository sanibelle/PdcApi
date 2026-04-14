using Pdc.Domain.Models.Versioning;

namespace Pdc.Domain.Models.MinisterialSpecification;

public class PerformanceCriteria : Changeable
{
    public required int Position { get; set; }
}
