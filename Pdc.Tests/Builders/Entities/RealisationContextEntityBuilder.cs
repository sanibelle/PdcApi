using Pdc.Infrastructure.Entities.MinisterialSpecification;
using Pdc.Infrastructure.Entities.Versioning;

namespace Pdc.Tests.Builders.Entities;

public class RealisationContextEntityBuilder
{
    private Guid _id = Guid.NewGuid();
    private string _value = "Default value";
    private List<ComplementaryInformationEntity> _complementaryInformations = new List<ComplementaryInformationEntity>();
    private CompetencyEntity? _competency = null;

    public RealisationContextEntityBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public RealisationContextEntityBuilder WithCompetency(CompetencyEntity competency)
    {
        _competency = competency;
        return this;
    }

    public RealisationContextEntityBuilder WithValue(string value)
    {
        _value = value;
        return this;
    }

    public RealisationContextEntityBuilder WithComplementaryInformations(List<ComplementaryInformationEntity> complementaryInformations)
    {
        _complementaryInformations = complementaryInformations;
        return this;
    }

    public RealisationContextEntityBuilder AddComplementaryInformations(ComplementaryInformationEntity complementaryInformation)
    {
        _complementaryInformations.Add(complementaryInformation);
        return this;
    }

    public RealisationContextEntity Build()
    {
        return new RealisationContextEntity
        {
            Id = _id,
            Value = _value,
            ComplementaryInformations = _complementaryInformations,
            Competency = _competency
        };
    }


}
