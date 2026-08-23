namespace Pdc.Domain.Models.Common;

public class Units
{
    public Guid? Id { get; set; }
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

    /// <summary>
    /// Calculates the units based on the provided theory, laboratory, and personal work hours.
    /// </summary>
    /// <param name="theoryHours">The theory hours in this format : 1.33</param>
    /// <param name="laboratoryHours">The laboratory hours in this format : 1.33</param>
    /// <param name="personnalWorkHours">The personal work hours in this format : 1.33</param>
    public Units(string theoryHours, string laboratoryHours, string personnalWorkHours)
    {
        int wholeUnits = 0;
        int numerator = 0;
        wholeUnits += ConvertWholeUnitsValue(theoryHours);
        wholeUnits += ConvertWholeUnitsValue(laboratoryHours);
        wholeUnits += ConvertWholeUnitsValue(personnalWorkHours);
        numerator += ConvertNumeratorValue(theoryHours);
        numerator += ConvertNumeratorValue(laboratoryHours);
        numerator += ConvertNumeratorValue(personnalWorkHours);
        if (numerator >= 3)
        {
            wholeUnits += numerator / 3;
            numerator = numerator % 3;
        }

        WholeUnit = wholeUnits;
        Numerator = numerator;
        Denominator = 3;
    }

    private int ConvertWholeUnitsValue(string value)
    {
        // TODO s'assurer que le format des heures a une , ou un . pour séparer les décimales
        string numerator = value.Split(",")[0];
        if (int.TryParse(numerator, out int result))
        {
            return result;
        }
        else
        {
            throw new InvalidCastException($"Invalid numerator value: {value} when trying to convert into Units");
        }
    }

    private int ConvertNumeratorValue(string value)
    {
        if (!value.Contains(","))
            return 0;
        switch (value.Split(",")[1])
        {
            case "0":
                return 0;
            case "33":
                return 1;
            case "66":
                return 2;
            default:
                throw new InvalidCastException($"Invalid numerator value: {value} when trying to convert into Units");
        }
    }
}
