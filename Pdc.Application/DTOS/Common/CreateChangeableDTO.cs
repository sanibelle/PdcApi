namespace Pdc.Application.DTOS.Common;

/// <summary>
/// Used for CompetencyElements and Performance criterias.
/// </summary>
public class CreateChangeableDTO
{
    public required string Value { get; set; }
    public required int Position { get; set; }
    public required IEnumerable<CreateComplementaryInformationDTO> ComplementaryInformations { get; set; }
}