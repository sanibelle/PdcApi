using Pdc.Domain.Models.Security;
using Pdc.Domain.Models.Versioning;

namespace TestDataSeeder.Builders.Models;

public class ComplementaryInformationBuilder
{
    private Guid _id = Guid.NewGuid();
    private string _text = "Test DATA";
    private DateTime _modifiedOn = DateTime.Now;
    private ChangeRecord? _writtenOnVersion;
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

    public ComplementaryInformationBuilder WithChangeRecord(ChangeRecord writtenOnVersion)
    {
        _writtenOnVersion = writtenOnVersion;
        return this;
    }

    public ComplementaryInformation Build()
    {
        return new ComplementaryInformation
        {
            Id = _id,
            Text = _text,
            ModifiedOn = _modifiedOn,
            WrittenOnVersion = _writtenOnVersion,
            CreatedBy = _createdBy,
            CreatedOn = DateTime.Now
        };
    }

}
