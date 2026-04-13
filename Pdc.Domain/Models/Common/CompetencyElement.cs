using Pdc.Domain.Models.Versioning;

namespace Pdc.Domain.Models.MinisterialSpecification;

public class CompetencyElement : Changeable
{
    public required int Position { get; set; }
}
