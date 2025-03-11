namespace Pdc.Domain.Entities.Common;

public class PerformanceCriteria : Changeable
{
    public required int Position { get; set; }
    public required IEnumerable<ContentSpecification> ContentSpecifications { get; set; }
}
