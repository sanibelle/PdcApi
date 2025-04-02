using Pdc.Domain.Models.MinisterialSpecification;
using Pdc.Domain.Models.Versioning;

namespace Pdc.Tests.Builders.Models;

public class MinisterialCompetencyElementBuilder
{
    private List<PerformanceCriteria> _performanceCriterias = new List<PerformanceCriteria>();
    private List<ComplementaryInformation> _complementaryInformations = new List<ComplementaryInformation>();
    private Guid _id = Guid.NewGuid();
    private int _position = 0;
    private string _value = string.Empty;

    public MinisterialCompetencyElementBuilder() { }

    public MinisterialCompetencyElementBuilder WithPerformanceCriterias(List<PerformanceCriteria> performanceCriterias)
    {
        _performanceCriterias = performanceCriterias;
        return this;
    }

    public MinisterialCompetencyElementBuilder WithComplementaryInformations(List<ComplementaryInformation> complementaryInformations)
    {
        _complementaryInformations = complementaryInformations;
        return this;
    }

    public MinisterialCompetencyElementBuilder AddComplementaryInformation(ComplementaryInformation complementaryInformation)
    {
        _complementaryInformations.Add(complementaryInformation);
        return this;
    }

    public MinisterialCompetencyElementBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public MinisterialCompetencyElementBuilder WithPosition(int position)
    {
        _position = position;
        return this;
    }

    public MinisterialCompetencyElementBuilder WithValue(string value)
    {
        _value = value;
        return this;
    }
    public MinisterialCompetencyElementBuilder AddPerformanceCriteria(PerformanceCriteria performanceCriteria)
    {
        _performanceCriterias.Add(performanceCriteria);
        return this;
    }

    public MinisterialCompetencyElement Build()
    {
        return new MinisterialCompetencyElement
        {
            ComplementaryInformations = _complementaryInformations,
            Id = _id,
            Position = _position,
            Value = _value,
            PerformanceCriterias = _performanceCriterias
        };
    }

}
