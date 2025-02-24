namespace Pdc.Domain.Entities.Common;

public abstract class ContentElement : Changeable
{
    // Name Diagrammes UML
    public required IEnumerable<ContentSpecification> ContentSpecifications { get; set; } //colonne 4.

}
