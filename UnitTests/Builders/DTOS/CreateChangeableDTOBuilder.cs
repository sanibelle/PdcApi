using Pdc.Application.DTOS.Common;

namespace Pdc.Tests.Builders.DTOS;

public class CreateChangeableDTOBuilder
{
    private string _value = "Default Value";
    private int _position = 0;
    private List<CreateComplementaryInformationDTO> _complementaryInformations = new List<CreateComplementaryInformationDTO>();

    public CreateChangeableDTOBuilder WithValue(string value)
    {
        _value = value;
        return this;
    }

    public CreateChangeableDTOBuilder WithPosition(int position)
    {
        _position = position;
        return this;
    }

    public CreateChangeableDTOBuilder WithComplementaryInformations(List<CreateComplementaryInformationDTO> complementaryInformations)
    {
        _complementaryInformations = complementaryInformations;
        return this;
    }

    public CreateChangeableDTO Build()
    {
        return new CreateChangeableDTO
        {
            Value = _value,
            Position = _position,
            ComplementaryInformations = _complementaryInformations
        };
    }
}

