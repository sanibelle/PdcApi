namespace Pdc.Domain.Models.Versioning;

/// <summary>
/// Nommé ainsi pour les confilts de noms avec System.Version
/// </summary>
public class ChangeRecord
{
    public int VersionNumber { get; set; }
    private IEnumerable<ComplementaryInformation> _complementaryInformations { get; set; } = new List<ComplementaryInformation>();
    private IEnumerable<ChangeDetail> _changeDetails { get; set; } = new List<ChangeDetail>();
    public Guid Id { get; set; }
    public DateTime CreatedOn { get; set; }
    /// <summary>
    /// Premet d'avoir plusieurs copies d'une version. Un seul est actif à la fois.
    /// </summary>
    public bool IsDraft { get; set; }
    //UTILISATEUR CreatedBy
    public string? Description { get; set; }
    /// <summary>
    /// The version before
    /// </summary>
    public ChangeRecord? ParentVersion { get; set; }
    /// <summary>
    /// The version with the changes
    /// </summary>
    public ChangeRecord? NextVersion { get; set; }
}
