using Pdc.Domain.Models.Security;

namespace Pdc.Domain.Models.Versioning;

/// <summary>
/// Nommé ainsi pour les confilts de noms avec System.Version
/// </summary>
public class ChangeRecord
{
    public int VersionNumber { get; set; }
    private IEnumerable<ComplementaryInformation> _complementaryInformations { get; set; } = new List<ComplementaryInformation>();
    private IEnumerable<ChangeDetail> _changeDetails { get; set; } = new List<ChangeDetail>();
    public Guid? Id { get; set; }
    public DateTime CreatedOn { get; set; }
    /// <summary>
    /// Premet d'avoir plusieurs copies d'une version. Un seul est actif à la fois.
    /// </summary>
    public bool IsDraft { get; set; }
    public string? Description { get; set; }
    /// <summary>
    /// The version before
    /// </summary>
    public ChangeRecord? ParentVersion { get; set; }
    /// <summary>
    /// The version with the changes
    /// </summary>
    public ChangeRecord? NextVersion { get; set; }
    /// <summary>
    /// Who created the version
    /// </summary>
    public User? CreatedBy { get; set; } // TODO faire un useCase pour valider la version.
    /// <summary>
    /// Who validated the version (changed IsDraft to false)
    /// </summary>
    public User? ValidatedBy { get; set; } // TODO faire un useCase pour valider la version.
    /// <summary>
    /// When the version changed IsDraft to false
    /// </summary>
    public DateTime? ValidatedOn { get; set; }

    /// <summary>
    /// Creates a default version with the version number statring at 1
    /// </summary>
    public ChangeRecord(User createdBy)
    {
        VersionNumber = 1;
        CreatedOn = DateTime.Now;
        IsDraft = true;
        CreatedBy = createdBy;
    }

    public void SetCreatedByOnUntracked(User user)
    {
        if (CreatedBy != null)
        {
            CreatedBy = user;
        }
    }
}
