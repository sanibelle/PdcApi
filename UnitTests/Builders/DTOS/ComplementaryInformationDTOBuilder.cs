using Pdc.Application.DTOS.Common;

namespace Pdc.Tests.Builders.DTOS;

public class ComplementaryInformationDTOBuilder
{
    private Guid _id = Guid.NewGuid();
    private string _text = "Test DATA";
    private DateTime _modifiedOn;
    private int _versionNumber;

    public ComplementaryInformationDTOBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public ComplementaryInformationDTOBuilder WithVersionNumber(int versionNumber)
    {
        _versionNumber = versionNumber;
        return this;
    }

    public ComplementaryInformationDTOBuilder WithText(string text)
    {
        _text = text;
        return this;
    }

    public ComplementaryInformationDTO Build()
    {
        return new ComplementaryInformationDTO
        {
            Id = _id,
            Text = _text,
            ModifiedOn = _modifiedOn,
            WrittenOnVersion = _versionNumber
        };
    }
}
