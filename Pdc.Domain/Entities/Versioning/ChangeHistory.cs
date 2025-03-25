namespace Pdc.Domain.Entities.Versioning;

public class ChangeHistory
{
    public required Guid Id { get; set; }
    private ICollection<ChangeDetail> _changeDetails { get; set; } = new List<ChangeDetail>();
    /// <summary>
    /// The version before
    /// </summary>
    public required ChangeRecord From { get; set; }
    /// <summary>
    /// The version with the changes
    /// </summary>
    public required ChangeRecord To { get; set; }
}
