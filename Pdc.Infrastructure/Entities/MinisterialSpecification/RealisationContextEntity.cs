using Pdc.Infrastructure.Entities.Versioning;

namespace Pdc.Infrastructure.Entities.MinisterialSpecification;

public class RealisationContextEntity : ChangeableEntity
{
    public required CompetencyEntity Competency { get; set; }
}
