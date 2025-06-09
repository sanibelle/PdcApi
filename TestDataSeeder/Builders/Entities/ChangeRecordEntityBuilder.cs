using Pdc.Infrastructure.Entities.Identity;
using Pdc.Infrastructure.Entities.Versioning;

namespace TestDataSeeder.Builders.Entities;

public class ChangeRecordEntityBuilder
{
    private Guid _id = Guid.NewGuid();
    private List<ChangeDetailEntity> _changeDetails = new List<ChangeDetailEntity>();
    private List<ComplementaryInformationEntity> _complementaryInformations = new List<ComplementaryInformationEntity>();
    private DateTime _createdOn = new DateTime(2025, 04, 07);
    private IdentityUserEntity _createdBy = new IdentityUserEntityBuilder().Build();
    private string _description = $"This is a test description value {Random.Shared.Next(0, 1000)}";
    private bool _isDraft = true;
    private ChangeRecordEntity? _nextVersion = null;
    private ChangeRecordEntity? _parentVersion = null;
    private int _versionNumber = 1;
    private IdentityUserEntity _validatedBy = null;

    public ChangeRecordEntityBuilder() { }

    public ChangeRecordEntityBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public ChangeRecordEntityBuilder WithChangeDetails(List<ChangeDetailEntity> changeDetails)
    {
        _changeDetails = changeDetails;
        return this;
    }

    public ChangeRecordEntityBuilder WithComplementaryInformations(List<ComplementaryInformationEntity> complementaryInformations)
    {
        _complementaryInformations = complementaryInformations;
        return this;
    }

    public ChangeRecordEntityBuilder AddChangeDetails(ChangeDetailEntity changeDetail)
    {
        _changeDetails.Add(changeDetail);
        return this;
    }

    public ChangeRecordEntityBuilder AddComplementaryInformations(ComplementaryInformationEntity complementaryInformation)
    {
        _complementaryInformations.Add(complementaryInformation);
        return this;
    }

    public ChangeRecordEntityBuilder WithCreatedOn(DateTime createdOn)
    {
        _createdOn = createdOn;
        return this;
    }

    public ChangeRecordEntityBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public ChangeRecordEntityBuilder WithIsDraft(bool isDraft)
    {
        _isDraft = isDraft;
        return this;
    }

    public ChangeRecordEntityBuilder WithNextVersion(ChangeRecordEntity nextVersion)
    {
        _nextVersion = nextVersion;
        return this;
    }

    public ChangeRecordEntityBuilder WithParentVersion(ChangeRecordEntity parentVersion)
    {
        _parentVersion = parentVersion;
        return this;
    }

    public ChangeRecordEntityBuilder WithVersionNumber(int versionNumber)
    {
        _versionNumber = versionNumber;
        return this;
    }

    public ChangeRecordEntityBuilder WithValidatedBy(IdentityUserEntity validatedBy)
    {
        _validatedBy = validatedBy;
        return this;
    }

    public ChangeRecordEntityBuilder WithCreatedBy(IdentityUserEntity createdBy)
    {
        _createdBy = createdBy;
        return this;
    }

    public ChangeRecordEntity Build()
    {
        return new ChangeRecordEntity
        {
            Id = _id,
            ChangeDetails = _changeDetails,
            ComplementaryInformations = _complementaryInformations,
            CreatedOn = _createdOn,
            Description = _description,
            IsDraft = _isDraft,
            NextVersion = _nextVersion,
            ParentVersion = _parentVersion,
            VersionNumber = _versionNumber,
            ValidatedBy = _validatedBy,
            CreatedBy = _createdBy
        };
    }
}

/// WIP builder d'entites (pas fini de les ajouter, ne pas oublier le name space)