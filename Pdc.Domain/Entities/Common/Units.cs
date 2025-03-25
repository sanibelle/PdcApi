namespace Pdc.Domain.Entities.Common;

public class Units
{
    public Guid Id { get; set; }
    public int WholeUnit { get; set; }
    public int? Numerator { get; set; }
    public int? Denominator { get; set; }

    public Units()
    {
    }

    public Units(int wholeUnits)
    {
        WholeUnit = wholeUnits;
    }

    public Units(int wholeUnits, int numerator, int denominator)
    {
        WholeUnit = wholeUnits;
        Numerator = numerator;
        Denominator = denominator;
    }
}
