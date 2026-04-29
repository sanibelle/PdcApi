using Pdc.Domain.Enums;

namespace Pdc.Domain.DTOS.Common;

/// <summary>
/// Used for CompetencyElements and Performance criterias.
/// </summary>
public class ChangeDetailDTO
{
    public Guid Id { get; set; }
    public required Guid ChangeableId { get; set; }
    public required ChangeType ChangeType { get; set; }
    public string? OldValue { get; set; }
}