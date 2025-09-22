using Pdc.Domain.Enums;

namespace Pdc.Infrastructure.Entities.Versioning;

public class ChangeDetailEntity
{
    public Guid? Id { get; set; }
    public required ChangeRecordEntity ChangeRecord { get; set; }
    public required ChangeableEntity Changeable { get; set; }
    public required ChangeType ChangeType { get; set; }
    /// <summary>
    /// Holds the old value of the property when updated or deleted.
    /// </summary>
    public string? OldValue { get; set; }

}
//TODO quand je vais tester. Un delete après un update devrait montrer la version initiale (pas l'entre deux)
// voir le diagramme UML pour comprendre.AncientValue : "Valeur initiale"
