using Pdc.Domain.Enums;

namespace Pdc.Domain.Models.Versioning;

public class ChangeDetail
{
    public Guid? Id { get; set; }
    public required ChangeRecord ChangeRecord { get; set; }
    public required Changeable Changeable { get; set; }
    public required ChangeType ChangeType { get; set; }
    /// <summary>
    /// Holds the old value of the property when updated or deleted.
    /// </summary>
    public string? OldValue { get; set; }
}
