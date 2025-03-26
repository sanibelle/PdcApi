using Pdc.Domain.Entities.Versioning;

namespace Pdc.Domain.Entities.MinisterialSpecification;

public class MinisterialRealisationContext : Changeable
{
    public required CompetencyElement CompetencyElement { get; set; }
}
