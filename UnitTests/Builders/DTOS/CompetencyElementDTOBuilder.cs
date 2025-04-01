using Pdc.Application.DTOS.Common;

namespace Pdc.Tests.Builders.DTOS;

public class CompetencyElementDTOBuilder : ChangeableDTOBuilder
{
    private IEnumerable<ChangeableDTO> _performanceCriterias = new List<ChangeableDTO>();

    public ChangeableDTOBuilder WithComplementaryInformations(List<ChangeableDTO> performanceCriterias)
    {
        _performanceCriterias = performanceCriterias;
        return this;
    }
    public CompetencyElementDTOBuilder WithPerformanceCriterias(List<ChangeableDTO> performanceCriterias)
    {
        _performanceCriterias = performanceCriterias;
        return this;
    }

    public CompetencyElementDTO BuildCompetencyElement()
    {
        ChangeableDTO changeable = base.Build();
        return new CompetencyElementDTO
        {
            Id = changeable.Id,
            Value = changeable.Value,
            Position = changeable.Position,
            ComplementaryInformations = changeable.ComplementaryInformations,
            PerformanceCriterias = _performanceCriterias
        };
    }

}
