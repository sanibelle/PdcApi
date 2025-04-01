using Pdc.Domain.Models.Common;
using Pdc.Domain.Models.Versioning;

namespace Pdc.Tests.Builders.Models;

public class RealisationContextBuilder
{
    private Guid _id = Guid.NewGuid();
    private string _value = "Default value";
    private List<ComplementaryInformation> _complementaryInformations = new List<ComplementaryInformation>();

    public RealisationContextBuilder() { }

    public RealisationContextBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public RealisationContextBuilder WithValue(string value)
    {
        _value = value;
        return this;
    }

    public RealisationContextBuilder WithComplementaryInformations(List<ComplementaryInformation> complementaryInformations)
    {
        _complementaryInformations = complementaryInformations;
        return this;
    }

    public RealisationContext Build()
    {
        return new RealisationContext
        {
            Id = _id,
            Value = _value,
            ComplementaryInformations = _complementaryInformations
        };
    }
}
