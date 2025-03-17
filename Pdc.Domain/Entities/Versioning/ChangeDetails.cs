namespace Pdc.Domain.Entities.Versioning;

public class ChangeDetails
{
    public required Guid Id { get; set; }
    public bool? IsApproved { get; set; }
    // TODO gestion de l'approbation des changements
    public Guid ApprovedBy { get; set; }
    public DateTime ApprovedOn { get; set; }
    public required ChangeHistory History { get; set; }
    public required AChangeable OldVersion { get; set; }
    public required AChangeable NewVersion { get; set; }
}
