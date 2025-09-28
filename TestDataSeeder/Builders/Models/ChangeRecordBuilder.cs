using Pdc.Domain.Models.Security;
using Pdc.Domain.Models.Versioning;

namespace TestDataSeeder.Builders.Models;

public class ChangeRecordBuilder
{
    private Guid _id = Guid.NewGuid();
    private DateTime _createdOn = new DateTime(2024, 04, 01);
    private int _versionNumber = 1;
    private string _description = "Test description";
    private bool _isDraft = false;
    private ChangeRecord? _nextVersion = null;
    private ChangeRecord? _parentVersion = null;
    private User _validatedBy;
    private User _createdBy;

    public ChangeRecordBuilder() { }

    public ChangeRecordBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public ChangeRecordBuilder WithCreatedOn(DateTime createdOn)
    {
        _createdOn = createdOn;
        return this;
    }

    public ChangeRecordBuilder WithVersionNumber(int versionNumber)
    {
        _versionNumber = versionNumber;
        return this;
    }

    public ChangeRecordBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public ChangeRecordBuilder WithIsDraft(bool isDraft)
    {
        _isDraft = isDraft;
        return this;
    }

    public ChangeRecordBuilder WithNextVersion(ChangeRecord nextVersion)
    {
        _nextVersion = nextVersion;
        return this;
    }

    public ChangeRecordBuilder WithParentVersion(ChangeRecord parentVersion)
    {
        _parentVersion = parentVersion;
        return this;
    }

    public ChangeRecordBuilder WithValidatedBy(User validatedBy)
    {
        _validatedBy = validatedBy;
        return this;
    }

    public ChangeRecordBuilder WithCreatedBy(User createdBy)
    {
        _createdBy = createdBy;
        return this;
    }

    public ChangeRecord Build()
    {
        return new ChangeRecord(_createdBy)
        {
            Id = _id,
            CreatedOn = _createdOn,
            VersionNumber = _versionNumber,
            Description = _description,
            IsDraft = _isDraft,
            NextVersion = _nextVersion,
            ParentVersion = _parentVersion,
            ValidatedBy = _validatedBy
        };
    }
}
