using Pdc.Infrastructure.Entities.Versioning;

namespace Pdc.Infrastructure.Entities.MinisterialSpecification;

public class PerformanceCriteriaEntity : ChangeableEntity
{
    public required int Position { get; set; }
}
