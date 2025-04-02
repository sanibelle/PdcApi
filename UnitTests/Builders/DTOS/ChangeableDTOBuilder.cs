using Pdc.Application.DTOS.Common;

namespace Pdc.Tests.Builders.DTOS;

public class ChangeableDTOBuilder
{
    private Guid? _id = Guid.NewGuid();
    private string _value = "Default Value";
    private int _position = 1;
    protected List<ComplementaryInformationDTO> _complementaryInformations = new List<ComplementaryInformationDTO>();

    public ChangeableDTOBuilder WithId(Guid? id)
    {
        _id = id;
        return this;
    }

    public ChangeableDTOBuilder WithValue(string value)
    {
        _value = value;
        return this;
    }

    public ChangeableDTOBuilder WithPosition(int position)
    {
        _position = position;
        return this;
    }

    public ChangeableDTOBuilder WithComplementaryInformations(List<ComplementaryInformationDTO> complementaryInformations)
    {
        _complementaryInformations = complementaryInformations;
        return this;
    }

    internal ChangeableDTOBuilder AddComplementaryInformation(ComplementaryInformationDTO complementaryInformation)
    {
        _complementaryInformations.Add(complementaryInformation);
        return this;
    }


    public ChangeableDTO Build()
    {
        return new ChangeableDTO
        {
            Id = _id,
            Value = _value,
            Position = _position,
            ComplementaryInformations = _complementaryInformations,
        };
    }

}
