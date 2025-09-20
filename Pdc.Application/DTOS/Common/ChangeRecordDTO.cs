using Pdc.Domain.Models.Versioning;

namespace Pdc.Application.DTOS.Common;

/// <summary>
/// Used for CompetencyElements and Performance criterias.
/// </summary>
public class ChangeRecordDTO
{
    public Guid? Id { get; set; }
    public int VersionNumber { get; set; }
    public IEnumerable<ComplementaryInformation> _complementaryInformations { get; set; } = [];
    public IEnumerable<ChangeDetail> _changeDetails { get; set; } = [];
    public DateTime CreatedOn { get; set; }
    public bool IsDraft { get; set; }
    public string? Description { get; set; }
    public ChangeRecord? ParentVersion { get; set; }
    public ChangeRecord? NextVersion { get; set; }
    public UserDTO? ValidateBy { get; set; }
}