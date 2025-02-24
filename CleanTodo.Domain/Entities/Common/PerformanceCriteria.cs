namespace Pdc.Domain.Entities.Common;

public abstract class PerformanceCriteria : Changeable
{
    public required int Position { get; set; }
    public required IEnumerable<ContentSpecification> ContentElements { get; set; }
}
