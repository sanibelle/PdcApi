using Pdc.Domain.Models.Versioning;

namespace Pdc.Application.DTOS.Common;

/// <summary>
/// Used for CompetencyElements and Performance criterias.
/// </summary>
public class ChangeRecordDTO
{
    public Guid Id { get; set; }
    public int VersionNumber { get; set; }
    private IEnumerable<ComplementaryInformation> _complementaryInformations { get; set; } = new List<ComplementaryInformation>();
    private IEnumerable<ChangeDetail> _changeDetails { get; set; } = new List<ChangeDetail>();
    public DateTime CreatedOn { get; set; }
    public bool IsDraft { get; set; }
    //UTILISATEUR CreatedBy
    public string? Description { get; set; }
    public ChangeRecord? ParentVersion { get; set; }
    public ChangeRecord? NextVersion { get; set; }
}