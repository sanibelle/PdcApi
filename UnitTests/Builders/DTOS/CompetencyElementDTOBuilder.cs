using Pdc.Application.DTOS.Common;

namespace Pdc.Tests.Builders.DTOS;

public class CompetencyElementDTOBuilder
{
    private Guid? _id = Guid.NewGuid();
    private string _value = "Default Value";
    private int _position = 1;
    protected List<ComplementaryInformationDTO> ComplementaryInformations = new List<ComplementaryInformationDTO>();
    private ICollection<ChangeableDTO> _performanceCriterias = new List<ChangeableDTO>();

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

    internal CompetencyElementDTOBuilder AddComplementaryInformation(ComplementaryInformationDTO complementaryInformation)
    {
        ComplementaryInformations.Add(complementaryInformation);
        return this;
    }

    public CompetencyElementDTOBuilder WithComplementaryInformations(List<ChangeableDTO> performanceCriterias)
    {
        _performanceCriterias = performanceCriterias;
        return this;
    }
    public CompetencyElementDTOBuilder WithPerformanceCriterias(List<ChangeableDTO> performanceCriterias)
    {
        _performanceCriterias = performanceCriterias;
        return this;
    }

    public CompetencyElementDTOBuilder AddComplementaryInformations(ComplementaryInformationDTO complementaryInformation)
    {
        ComplementaryInformations.Add(complementaryInformation);
        return this;
    }
    public CompetencyElementDTOBuilder AddPerformanceCriteria(ChangeableDTO performanceCriteria)
    {
        _performanceCriterias.Add(performanceCriteria);
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
