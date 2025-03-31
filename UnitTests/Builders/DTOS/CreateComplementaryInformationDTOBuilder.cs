using Pdc.Application.DTOS.Common;

namespace Pdc.Tests.Builders.DTOS;

public class CreateComplementaryInformationDTOBuilder
{
    private int _versionNumber = 1;
    private DateTime _modifiedOn = DateTime.UtcNow;
    private string _text = "Default Text";

    public CreateComplementaryInformationDTOBuilder WithVersionNumber(int versionNumber)
    {
        _versionNumber = versionNumber;
        return this;
    }

    public CreateComplementaryInformationDTOBuilder WithModifiedOn(DateTime modifiedOn)
    {
        _modifiedOn = modifiedOn;
        return this;
    }

    public CreateComplementaryInformationDTOBuilder WithText(string text)
    {
        _text = text;
        return this;
    }

    public CreateComplementaryInformationDTO Build()
    {
        return new CreateComplementaryInformationDTO
        {
            VersionNumber = _versionNumber,
            ModifiedOn = _modifiedOn,
            Text = _text
        };
    }
}
