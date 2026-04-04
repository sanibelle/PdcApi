using Pdc.Domain.Models.Versioning;
using Pdc.Domain.Models.Security;

namespace TestDataSeeder.Builders.Models;

public class ChangeRecordBuilder
{
    private Guid _id = Guid.NewGuid();
    private DateTime _createdOn = new DateTime(2024, 04, 01);
    private int _changeRecordNumber = 1;
    private string _description = "Test description";
    private bool _isDraft = false;
    private ChangeRecord? _nextChangeRecord = null;
    private ChangeRecord? _parentChangeRecord = null;
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

    public ChangeRecordBuilder WithChangeRecordNumber(int changeRecordNumber)
    {
        _changeRecordNumber = changeRecordNumber;
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

    public ChangeRecordBuilder WithNextChangeRecord(ChangeRecord nextChangeRecord)
    {
        _nextChangeRecord = nextChangeRecord;
        return this;
    }

    public ChangeRecordBuilder WithParentChangeRecord(ChangeRecord parentChangeRecord)
    {
        _parentChangeRecord = parentChangeRecord;
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
            ChangeRecordNumber = _changeRecordNumber,
            Description = _description,
            IsDraft = _isDraft,
            NextChangeRecord = _nextChangeRecord,
            ParentChangeRecord = _parentChangeRecord,
            ValidatedBy = _validatedBy
        };
    }
}
