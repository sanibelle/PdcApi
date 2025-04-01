using Pdc.Domain.Enums;

namespace Pdc.Domain.Models.Versioning;

public class ChangeDetail
{
    public required Guid Id { get; set; }
    public required ChangeRecord ChangeRecord { get; set; }
    public required AChangeable Changeable { get; set; }
    public required ChangeType ChangeType { get; set; }
    /// <summary>
    /// Holds the old value of the property when updated or deleted.
    /// </summary>
    public string? OldValue { get; set; }
}
//TODO quand je vais tester. Un delete après un update devrait montrer la version initiale (pas l'entre deux)
// voir le diagramme UML pour comprendre.AncientValue : "Valeur initiale"
