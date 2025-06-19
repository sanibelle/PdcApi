using Pdc.Infrastructure.Entities.MinisterialSpecification;
using Pdc.Infrastructure.Entities.Versioning;

namespace TestDataSeeder.Builders.Entities;

public class PerformanceCriteriaEntityBuilder
{
    private List<ComplementaryInformationEntity> _complementaryInformations = new List<ComplementaryInformationEntity>();
    private Guid _id = Guid.NewGuid();
    private int _position = 0;
    private string _value = string.Empty;

    public PerformanceCriteriaEntityBuilder() { }

    public PerformanceCriteriaEntityBuilder WithComplementaryInformationEntitys(List<ComplementaryInformationEntity> complementaryInformations)
    {
        _complementaryInformations = complementaryInformations;
        return this;
    }

    public PerformanceCriteriaEntityBuilder AddComplementaryInformation(ComplementaryInformationEntity complementaryInformation)
    {
        _complementaryInformations.Add(complementaryInformation);
        return this;
    }

    public PerformanceCriteriaEntityBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public PerformanceCriteriaEntityBuilder WithPosition(int position)
    {
        _position = position;
        return this;
    }

    public PerformanceCriteriaEntityBuilder WithValue(string value)
    {
        _value = value;
        return this;
    }

    public PerformanceCriteriaEntity Build()
    {
        return new PerformanceCriteriaEntity
        {
            ComplementaryInformations = _complementaryInformations,
            Id = _id,
            Position = _position,
            Value = _value
        };
    }

}

/// WIP builder d'entites (pas fini de les ajouter, ne pas oublier le name space)