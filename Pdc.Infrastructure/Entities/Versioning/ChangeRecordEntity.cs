namespace Pdc.Infrastructure.Entities.Versioning;

/// <summary>
/// Nommé ainsi pour les confilts de noms avec System.Version
/// </summary>
public class ChangeRecordEntity
{
    public IEnumerable<ComplementaryInformationEntity> ComplementaryInformations { get; set; } = new List<ComplementaryInformationEntity>();
    public IEnumerable<ChangeDetailEntity> ChangeDetails { get; set; } = new List<ChangeDetailEntity>();
    public Guid Id { get; set; }
    public DateTime CreatedOn { get; set; }
    /// <summary>
    /// Premet d'avoir plusieurs copies d'une version. Un seul est actif à la fois.
    /// </summary>
    public bool IsDraft { get; set; }
    //UTILISATEUR CreatedBy
    public int VersionNumber { get; set; }
    public string? Description { get; set; }
    /// <summary>
    /// The version before
    /// </summary>
    public ChangeRecordEntity? ParentVersion { get; set; }
    /// <summary>
    /// The version with the changes
    /// </summary>
    public ChangeRecordEntity? NextVersion { get; set; }

    public ChangeRecordEntity()
    {
        CreatedOn = DateTime.Now;
    }
}
