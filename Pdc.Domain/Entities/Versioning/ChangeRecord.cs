namespace Pdc.Domain.Entities.Versioning;

/// <summary>
/// Nommé ainsi pour les confilts de noms avec System.Version
/// </summary>
public class ChangeRecord
{
    private ICollection<ComplementaryInformation> complementaryInformations { get; set; } = new List<ComplementaryInformation>();
    public Guid Id { get; set; }
    public DateTime CreatedOn { get; set; }
    /// <summary>
    /// Premet d'avoir plusieurs copies d'une version. Un seul est actif à la fois.
    /// </summary>
    public bool IsDraft { get; set; }
    //UTILISATEUR CreatedBy
    public required string VersionNumber { get; set; }
    public string? Description { get; set; }
    public required ChangeHistory ChangeHistory { get; set; }
}
