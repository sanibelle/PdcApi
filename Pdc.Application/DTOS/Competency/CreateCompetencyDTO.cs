using Pdc.Application.DTOS.Common;
using Pdc.Domain.Entities.Common;

namespace Pdc.Application.DTOS;

public class CreateCompetencyDTO
{
    public required string Code { get; set; }
    public Units? Units { get; set; } = null;
    public bool IsMandatory { get; set; }
    public bool IsOptionnal { get; set; }
    public required string StatementOfCompetency { get; set; }
    public IEnumerable<CreateChangeableDTO> RealisationContexts { get; set; } = new List<CreateChangeableDTO>();
    public IEnumerable<CreateCompetencyElementDTO> CompetencyElements { get; set; } = new List<CreateCompetencyElementDTO>();

    public CreateCompetencyDTO() { }
}
