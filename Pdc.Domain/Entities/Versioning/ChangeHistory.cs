namespace Pdc.Domain.Entities.Versioning;

public class ChangeHistory
{
    public required Guid Id { get; set; }
    /// <summary>
    /// The initiator of the change
    /// </summary>
    public required AChangeable Initiator { get; set; } // TODO : Et si je passais <T>, pour être plus générique ? COmment le gérer dans la base de données ?
    /// <summary>
    /// The impacted entity by the change
    /// </summary>
    public required ChangeVersion From { get; set; }
    public required ChangeVersion To { get; set; }
    public required AChangeable Impacted { get; set; }
    public required DateTime CreatedOn { get; set; }
    public required List<ChangeDetails> Changes { get; set; }
}
