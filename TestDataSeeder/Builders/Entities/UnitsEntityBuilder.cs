using Pdc.Infrastructure.Entities.MinisterialSpecification;

namespace TestDataSeeder.Builders.Entities;

public class UnitsEntityBuilder
{
    private Guid? _id;
    private int? _numerator;
    private int? _denominator;
    private int _wholeUnit = 1;

    public UnitsEntityBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public UnitsEntityBuilder WithNumerator(int numerator)
    {
        _numerator = numerator;
        return this;
    }

    public UnitsEntityBuilder WithDenominator(int denominator)
    {
        _denominator = denominator;
        return this;
    }

    public UnitsEntityBuilder WithWholeUnit(int wholeUnit)
    {
        _wholeUnit = wholeUnit;
        return this;
    }

    public UnitsEntity Build()
    {
        return new UnitsEntity
        {
            Id = _id,
            Denominator = _denominator,
            Numerator = _numerator,
            WholeUnit = _wholeUnit
        };
    }
}
