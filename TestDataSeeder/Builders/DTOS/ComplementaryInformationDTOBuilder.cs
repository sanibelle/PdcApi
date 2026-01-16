using Pdc.Domain.DTOS.Common;

namespace TestDataSeeder.Builders.DTOS;

public class ComplementaryInformationDTOBuilder
{
    private Guid? _id;
    private string _text = "Test DATA";
    private DateTime _modifiedOn = new DateTime(2025, 04, 06, 0, 0, 0, DateTimeKind.Utc);
    private DateTime _createdOn = new DateTime(2025, 04, 05, 0, 0, 0, DateTimeKind.Utc);
    private int? _versionNumber;
    private UserDTO? _createdBy = null;

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
            CreatedOn = _createdOn,
            CreatedBy =  _createdBy
        };
    }
}
