namespace Pdc.Application.DTOS.Common;

/// <summary>
/// Used for CompetencyElements and Performance criterias.
/// </summary>
public class ChangeableDTO
{
    public Guid? Id { get; set; }
    public required string Value { get; set; }
    public int? Position { get; set; }
    public IList<ComplementaryInformationDTO>? ComplementaryInformations { get; set; }
}