using Pdc.Domain.DTOS.Common;

namespace TestDataSeeder.Builders.DTOS;

public class ChangeableDTOBuilder<T>
{
    private Guid? _id = null;
    private T _value;
    private int? _position = null;
    protected List<ComplementaryInformationDTO>? _complementaryInformations = null;

    public ChangeableDTOBuilder<T> WithId(Guid? id)
    {
        _id = id;
        return this;
    }

    public ChangeableDTOBuilder<T> WithValue(T value)
    {
        _value = value;
        return this;
    }

    public ChangeableDTOBuilder<T> WithPosition(int position)
    {
        _position = position;
        return this;
    }

    public ChangeableDTOBuilder<T> WithComplementaryInformations(List<ComplementaryInformationDTO> complementaryInformations)
    {
        _complementaryInformations = complementaryInformations;
        return this;
    }

    public ChangeableDTOBuilder<T> AddComplementaryInformation(ComplementaryInformationDTO complementaryInformation)
    {
        if (_complementaryInformations == null)
            _complementaryInformations = new List<ComplementaryInformationDTO>();
        _complementaryInformations.Add(complementaryInformation);
        return this;
    }


    public ChangeableDTO<T> Build()
    {
        return new ChangeableDTO<T>
        {
            Id = _id,
            Value = _value,
            Position = _position,
            ComplementaryInformations = _complementaryInformations,
        };
    }

}
