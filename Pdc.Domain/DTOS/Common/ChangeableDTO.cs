namespace Pdc.Domain.DTOS.Common;

/// <summary>
/// Used for CompetencyElements and Performance criterias.
/// </summary>
public class ChangeableDTO<T>
{
    public Guid? Id { get; set; }
    public required T Value { get; set; }
    public int? Position { get; set; }
    public IList<ComplementaryInformationDTO>? ComplementaryInformations { get; set; } = [];
}