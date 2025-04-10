using Pdc.Infrastructure.Entities.Versioning;

namespace Pdc.Tests.Builders.Entities;

public class ComplementaryInformationEntityBuilder
{
    private Guid _id = Guid.NewGuid();
    private string _text = $"This is a test text value {Random.Shared.Next(0, 1000)}";
    private ChangeableEntity _changeable;
    private DateTime _modifiedOn = new DateTime(2025, 04, 07);
    private ChangeRecordEntity _writtenOnVersion;

    public ComplementaryInformationEntityBuilder() { }

    public ComplementaryInformationEntityBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public ComplementaryInformationEntityBuilder WithText(string text)
    {
        _text = text;
        return this;
    }

    public ComplementaryInformationEntityBuilder WithChangeable(ChangeableEntity changeable)
    {
        _changeable = changeable;
        return this;
    }

    public ComplementaryInformationEntityBuilder WithChangeRecord(ChangeRecordEntity writtenOnVersion)
    {
        _writtenOnVersion = writtenOnVersion;
        return this;
    }

    public ComplementaryInformationEntityBuilder WithModifiedOn(DateTime modifiedOn)
    {
        _modifiedOn = modifiedOn;
        return this;
    }

    public ComplementaryInformationEntity Build()
    {
        return new ComplementaryInformationEntity
        {
            Id = _id,
            Changeable = _changeable,
            Text = _text,
            ModifiedOn = _modifiedOn,
            WrittenOnVersion = _writtenOnVersion
        };
    }

}

/// WIP builder d'entites (pas fini de les ajouter, ne pas oublier le name space)