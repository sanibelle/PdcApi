using Pdc.Domain.Entities.Versioning;

namespace Pdc.Domain.Entities.Common;

public class PerformanceCriteria : AChangeable
{
    public required int Position { get; set; }
    public required IEnumerable<ComplementaryInformations> ContentSpecifications { get; set; }
}
