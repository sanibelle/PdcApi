using Pdc.Infrastructure.Entities.MinisterialSpecification;
using Pdc.Infrastructure.Entities.Versioning;

namespace Pdc.Tests.Builders.Entities;

public class CompetencyElementEntityBuilder
{
    private Guid _id = Guid.NewGuid();
    private List<PerformanceCriteriaEntity> _performanceCriterias = new List<PerformanceCriteriaEntity>();
    private List<ComplementaryInformationEntity> _complementaryInformations = new List<ComplementaryInformationEntity>();
    private int _position = 0;
    private string _value = string.Empty;

    public CompetencyElementEntityBuilder() { }

    public CompetencyElementEntityBuilder WithPerformanceCriteriaEntitys(List<PerformanceCriteriaEntity> performanceCriterias)
    {
        _performanceCriterias = performanceCriterias;
        return this;
    }

    public CompetencyElementEntityBuilder WithComplementaryInformationEntitys(List<ComplementaryInformationEntity> complementaryInformations)
    {
        _complementaryInformations = complementaryInformations;
        return this;
    }

    public CompetencyElementEntityBuilder AddComplementaryInformation(ComplementaryInformationEntity complementaryInformation)
    {
        _complementaryInformations.Add(complementaryInformation);
        return this;
    }

    public CompetencyElementEntityBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public CompetencyElementEntityBuilder WithPosition(int position)
    {
        _position = position;
        return this;
    }

    public CompetencyElementEntityBuilder WithValue(string value)
    {
        _value = value;
        return this;
    }
    public CompetencyElementEntityBuilder AddPerformanceCriteria(PerformanceCriteriaEntity performanceCriteria)
    {
        _performanceCriterias.Add(performanceCriteria);
        return this;
    }

    public CompetencyElementEntity Build()
    {
        return new CompetencyElementEntity
        {
            ComplementaryInformations = _complementaryInformations,
            Id = _id,
            Position = _position,
            Value = _value,
            PerformanceCriterias = _performanceCriterias
        };
    }

}

/// WIP builder d'entites (pas fini de les ajouter, ne pas oublier le name space)