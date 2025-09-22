using Pdc.Application.DTOS.Common;

namespace TestDataSeeder.Builders.DTOS;

public class CompetencyElementDTOBuilder
{
    private Guid? _id = null;
    private string _value = "Default Value";
    private int? _position = null;
    protected List<ComplementaryInformationDTO> ComplementaryInformations = [];
    private ICollection<ChangeableDTO> _performanceCriterias = [];

    public CompetencyElementDTOBuilder WithId(Guid? id)
    {
        _id = id;
        return this;
    }

    public CompetencyElementDTOBuilder WithValue(string value)
    {
        _value = value;
        return this;
    }

    public CompetencyElementDTOBuilder WithPosition(int position)
    {
        _position = position;
        return this;
    }

    public CompetencyElementDTOBuilder WithComplementaryInformations(List<ComplementaryInformationDTO> complementaryInformations)
    {
        ComplementaryInformations = complementaryInformations;
        return this;
    }

    public CompetencyElementDTOBuilder AddPerformanceCriteria(ChangeableDTO performanceCriteria)
    {
        _performanceCriterias.Add(performanceCriteria);
        return this;
    }
    public CompetencyElementDTOBuilder WithPerformanceCriterias(List<ChangeableDTO> performanceCriterias)
    {
        _performanceCriterias = performanceCriterias;
        return this;
    }
    public CompetencyElementDTOBuilder AddComplementaryInformation(ComplementaryInformationDTO complementaryInformation)
    {
        ComplementaryInformations.Add(complementaryInformation);
        return this;
    }

    public CompetencyElementDTO BuildCompetencyElement()
    {
        return new CompetencyElementDTO
        {
            Id = _id,
            Value = _value,
            Position = _position,
            ComplementaryInformations = ComplementaryInformations,
            PerformanceCriterias = _performanceCriterias
        };
    }

}
