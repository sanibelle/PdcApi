using Pdc.Infrastructure.Entities.Identity;

namespace Pdc.Infrastructure.Entities.Versioning;

/// <summary>
/// Nommé ainsi pour les confilts de noms avec System.Version
/// </summary>
public class ChangeRecordEntity
{
    public Guid? Id { get; set; }
    public virtual ICollection<ComplementaryInformationEntity>? ComplementaryInformations { get; set; }
    public virtual ICollection<ChangeDetailEntity>? ChangeDetails { get; set; }
    public DateTime CreatedOn { get; set; }
    public Guid CreatedById { get; set; }
    public virtual IdentityUserEntity? CreatedBy { get; set; }
    /// <summary>
    /// Premet d'avoir plusieurs copies d'une version. Un seul est actif à la fois.
    /// </summary>
    public bool IsDraft { get; set; }
    public int VersionNumber { get; set; }
    public string? Description { get; set; }
    /// <summary>
    /// The version before
    /// </summary>
    public virtual ChangeRecordEntity? ParentVersion { get; set; } = null;
    /// <summary>
    /// The version with the changes
    /// </summary>
    public virtual ChangeRecordEntity? NextVersion { get; set; } = null;
    public Guid? ValidatedById { get; set; }

    public virtual IdentityUserEntity? ValidatedBy { get; set; }
    public DateTime? ValidatedOn { get; set; }
    public Guid? NextVersionId { get; set; }
    public Guid? ParentVersionId { get; set; }

    public ChangeRecordEntity()
    {
        CreatedOn = DateTime.Now;
    }
}
