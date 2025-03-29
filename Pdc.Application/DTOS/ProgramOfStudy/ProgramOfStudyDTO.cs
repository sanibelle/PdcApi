using Pdc.Domain.Entities.Common;

namespace Pdc.Application.DTOS;

public class ProgramOfStudyDTO : CreateProgramOfStudyDTO
{
    /// <summary>
    /// Les unités des cours généraux
    /// </summary>
    public required Units GeneralUnits { get; set; }
    /// <summary>
    /// Les unités des cours complémentaires
    /// </summary>
    public required Units ComplementaryUnits { get; set; }
    public ProgramOfStudyDTO()
    {
    }
}
