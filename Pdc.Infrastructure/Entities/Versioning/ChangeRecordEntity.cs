using Pdc.Infrastructure.Entities.Identity;

namespace Pdc.Infrastructure.Entities.Versioning;

/// <summary>
/// Nommé ainsi pour les confilts de noms avec System.Version
/// </summary>
public class ChangeRecordEntity
{
    private Guid _id;
    public Guid Id
    {
        get => _id;
        set
        {
            if (Guid.Empty == value)
            {
                _id = Guid.NewGuid();
            }
            else
            {
                _id = value;
            }
        }
    }

    public IEnumerable<ComplementaryInformationEntity> ComplementaryInformations { get; set; } = new List<ComplementaryInformationEntity>();
    public IEnumerable<ChangeDetailEntity> ChangeDetails { get; set; } = new List<ChangeDetailEntity>();
    public DateTime CreatedOn { get; set; }
    public required IdentityUserEntity CreatedBy { get; set; }
    /// <summary>
    /// Premet d'avoir plusieurs copies d'une version. Un seul est actif à la fois.
    /// </summary>
    public bool IsDraft { get; set; }
    public int VersionNumber { get; set; }
    public string? Description { get; set; }
    /// <summary>
    /// The version before
    /// </summary>
    public ChangeRecordEntity? ParentVersion { get; set; } = null;
    /// <summary>
    /// The version with the changes
    /// </summary>
    public ChangeRecordEntity? NextVersion { get; set; } = null;
    public IdentityUserEntity? ValidatedBy { get; set; }
    public DateTime? ValidatedOn { get; set; }
    public ChangeRecordEntity()
    {
        CreatedOn = DateTime.Now;
        Id = Guid.NewGuid();
    }

    internal void SetCreatedBy(IdentityUserEntity createdBy)
    {
        CreatedBy = createdBy;
        ComplementaryInformations.ToList().ForEach(x => x.SetCreatedBy(createdBy));
    }
}
