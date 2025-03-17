namespace Pdc.Domain.Entities.Common;

public class Units
{
    public Guid Id { get; set; }
    public int WholeUnits { get; set; }
    public int? Numerator { get; set; }
    public int? Denominator { get; set; }

    public Units()
    {
    }

    public Units(int wholeUnits)
    {
        WholeUnits = wholeUnits;
    }

    public Units(int wholeUnits, int numerator, int denominator)
    {
        WholeUnits = wholeUnits;
        Numerator = numerator;
        Denominator = denominator;
    }
}
