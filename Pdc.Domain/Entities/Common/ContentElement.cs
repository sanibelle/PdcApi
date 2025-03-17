using Pdc.Domain.Entities.Versioning;

namespace Pdc.Domain.Entities.Common;

public class ContentElement : AChangeable
{
    // Name Diagrammes UML
    public required IEnumerable<ComplementaryInformations> ContentSpecifications { get; set; } //colonne 4.

}
