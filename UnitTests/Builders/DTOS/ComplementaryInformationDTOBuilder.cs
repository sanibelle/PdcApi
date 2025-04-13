using Pdc.Application.DTOS.Common;

namespace Pdc.Tests.Builders.DTOS;

public class ComplementaryInformationDTOBuilder
{
    private Guid? _id = Guid.NewGuid();
    private string _text = "Test DATA";
    private DateTime _modifiedOn = new DateTime(2025, 04, 05);
    private int _versionNumber;
    private UserDTO _createdBy;

    public ComplementaryInformationDTOBuilder WithId(Guid? id)
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

    public ComplementaryInformationDTOBuilder WithCreatedBy(UserDTO createdBy)
    {
        _createdBy = createdBy;
        return this;
    }

    public ComplementaryInformationDTO Build()
    {
        return new ComplementaryInformationDTO
        {
            Id = _id,
            Text = _text,
            ModifiedOn = _modifiedOn,
            WrittenOnVersion = _versionNumber,
            CreatedOn = DateTime.Now,
            CreatedBy =  _createdBy
        };
    }
}
