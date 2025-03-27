using Pdc.Infrastructure.Entities.Versioning;

namespace Pdc.Infrastructure.Entities.MinisterialSpecification;

public class MinisterialRealisationContextEntity : ChangeableEntity
{
    public required CompetencyElementEntity CompetencyElement { get; set; }
}
