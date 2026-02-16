using Pdc.Domain.Enums;
using Pdc.Domain.Models.Common;
using Pdc.Domain.Models.CourseFramework;
using Pdc.Domain.Models.MinisterialSpecification;

namespace TestDataSeeder.Builders.Models;

public class ProgramOfStudyBuilder
{
    private string _code = "TES" + new Random().Next(1000, 9999);
    private string _name = "Default Program";
    private ProgramType _programType = ProgramType.DEC;
    private int _monthsDuration = 36;
    private int _specificDurationHours = 2010;
    private int _totalDurationHours = 5730;
    private DateOnly _publishedOn = DateOnly.FromDateTime(DateTime.UtcNow);
    private List<MinisterialCompetency> _competencies = new List<MinisterialCompetency>();
    private Units _specificUnits = new Units(10);
    private Units _optionalUnits = new Units(5);
    private Units _generalUnits = new Units(15);
    private Units _complementaryUnits = new Units(5);

    public ProgramOfStudyBuilder() { }

    public ProgramOfStudyBuilder WithCode(string code)
    {
        _code = code;
        return this;
    }

    public ProgramOfStudyBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public ProgramOfStudyBuilder WithProgramType(ProgramType programType)
    {
        _programType = programType;
        return this;
    }

    public ProgramOfStudyBuilder WithMonthsDuration(int monthsDuration)
    {
        _monthsDuration = monthsDuration;
        return this;
    }

    public ProgramOfStudyBuilder WithSpecificDurationHours(int specificDurationHours)
    {
        _specificDurationHours = specificDurationHours;
        return this;
    }

    public ProgramOfStudyBuilder WithTotalDurationHours(int totalDurationHours)
    {
        _totalDurationHours = totalDurationHours;
        return this;
    }

    public ProgramOfStudyBuilder WithPublishedOn(DateOnly publishedOn)
    {
        _publishedOn = publishedOn;
        return this;
    }

    public ProgramOfStudyBuilder WithCompetencies(List<MinisterialCompetency> competencies)
    {
        _competencies = competencies;
        return this;
    }

    public ProgramOfStudyBuilder WithSpecificUnits(Units specificUnits)
    {
        _specificUnits = specificUnits;
        return this;
    }

    public ProgramOfStudyBuilder WithOptionalUnits(Units optionalUnits)
    {
        _optionalUnits = optionalUnits;
        return this;
    }

    public ProgramOfStudyBuilder WithGeneralUnits(Units generalUnits)
    {
        _generalUnits = generalUnits;
        return this;
    }

    public ProgramOfStudyBuilder WithComplementaryUnits(Units complementaryUnits)
    {
        _complementaryUnits = complementaryUnits;
        return this;
    }

    public ProgramOfStudy Build()
    {
        return new ProgramOfStudy
        {
            Code = _code,
            Name = _name,
            ProgramType = _programType,
            MonthsDuration = _monthsDuration,
            SpecificDurationHours = _specificDurationHours,
            TotalDurationHours = _totalDurationHours,
            PublishedOn = _publishedOn,
            Competencies = _competencies,
            SpecificUnits = _specificUnits,
            OptionalUnits = _optionalUnits,
            GeneralUnits = _generalUnits,
            ComplementaryUnits = _complementaryUnits
        };
    }

}
