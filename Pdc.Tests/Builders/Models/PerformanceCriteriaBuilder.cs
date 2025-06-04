using Pdc.Domain.Models.MinisterialSpecification;
using Pdc.Domain.Models.Versioning;

namespace Pdc.Tests.Builders.Models;

public class PerformanceCriteriaBuilder
{
    private Guid _id = Guid.NewGuid();
    private int _position = 1;
    private string _value = "Default value";
    private List<ComplementaryInformation> _complementaryInformations = new List<ComplementaryInformation>();

    public PerformanceCriteriaBuilder() { }

    public PerformanceCriteriaBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public PerformanceCriteriaBuilder WithPosition(int position)
    {
        _position = position;
        return this;
    }

    public PerformanceCriteriaBuilder WithValue(string value)
    {
        _value = value;
        return this;
    }

    public PerformanceCriteriaBuilder WithComplementaryInformations(List<ComplementaryInformation> complementaryInformations)
    {
        _complementaryInformations = complementaryInformations;
        return this;
    }

    public PerformanceCriteriaBuilder AddComplementaryInformations(ComplementaryInformation complementaryInformation)
    {
        _complementaryInformations.Add(complementaryInformation);
        return this;
    }

    public PerformanceCriteria Build()
    {
        return new PerformanceCriteria
        {
            Id = _id,
            Value = _value,
            Position = _position,
            ComplementaryInformations = _complementaryInformations
        };
    }

}
