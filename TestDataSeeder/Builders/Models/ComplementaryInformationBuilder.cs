using Pdc.Domain.Models.Versioning;
using Pdc.Domain.Models.Security;

namespace TestDataSeeder.Builders.Models;

public class ComplementaryInformationBuilder
{
    private Guid _id = Guid.NewGuid();
    private string _text = "Test DATA";
    private DateTime _modifiedOn = DateTime.UtcNow;
    private ChangeRecord? _writtenOnChangeRecord;
    private User _createdBy = new UserBuilder().Build();

    public ComplementaryInformationBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public ComplementaryInformationBuilder WithText(string text)
    {
        _text = text;
        return this;
    }

    public ComplementaryInformationBuilder WithCreatedBy(User createdBy)
    {
        _createdBy = createdBy;
        return this;
    }

    public ComplementaryInformationBuilder WithChangeRecord(ChangeRecord changeRecord)
    {
        _writtenOnChangeRecord = changeRecord;
        return this;
    }

    public ComplementaryInformation Build()
    {
        return new ComplementaryInformation
        {
            Id = _id,
            Text = _text,
            ModifiedOn = _modifiedOn,
            WrittenOnChangeRecord = _writtenOnChangeRecord,
            CreatedBy = _createdBy,
            CreatedOn = DateTime.UtcNow
        };
    }

}
