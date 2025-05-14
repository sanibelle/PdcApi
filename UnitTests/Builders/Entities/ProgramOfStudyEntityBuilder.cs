using Pdc.Domain.Enums;
using Pdc.Domain.Models.Common;
using Pdc.Infrastructure.Entities.CourseFramework;
using Pdc.Infrastructure.Entities.MinisterialSpecification;

namespace Pdc.Tests.Builders.Entities;

public class ProgramOfStudyEntityBuilder
{
    private string _code = "420.B0";
    private Units? _specificUnits = new Units(26, 2, 3);
    private Units? _optionnalUnits = new Units(16, 2, 3);
    private Units _generalUnits = new Units(16, 2, 3);
    private Units _complementaryUnits = new Units(4);
    private string _name = "Techniques de l'informatique";
    private ProgramType _programType = ProgramType.DEC;
    private int _monthsDuration = 36;
    private int _specificDurationHours = 2010;
    private int _totalDurationHours = 5730;
    private DateOnly _publishedOn = DateOnly.FromDateTime(DateTime.Now);
    private IList<CompetencyEntity> _competencies = new List<CompetencyEntity>();

    public ProgramOfStudyEntityBuilder WithCode(string code)
    {
        _code = code;
        return this;
    }

    public ProgramOfStudyEntityBuilder WithSpecificUnits(Units specificUnits)
    {
        _specificUnits = specificUnits;
        return this;
    }

    public ProgramOfStudyEntityBuilder WithOptionnalUnits(Units optionnalUnits)
    {
        _optionnalUnits = optionnalUnits;
        return this;
    }

    public ProgramOfStudyEntityBuilder WithGeneralUnits(Units generalUnits)
    {
        _generalUnits = generalUnits;
        return this;
    }

    public ProgramOfStudyEntityBuilder WithComplementaryUnits(Units complementaryUnits)
    {
        _complementaryUnits = complementaryUnits;
        return this;
    }

    public ProgramOfStudyEntityBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public ProgramOfStudyEntityBuilder WithProgramType(ProgramType programType)
    {
        _programType = programType;
        return this;
    }

    public ProgramOfStudyEntityBuilder WithMonthsDuration(int monthsDuration)
    {
        _monthsDuration = monthsDuration;
        return this;
    }

    public ProgramOfStudyEntityBuilder WithSpecificDurationHours(int specificDurationHours)
    {
        _specificDurationHours = specificDurationHours;
        return this;
    }

    public ProgramOfStudyEntityBuilder WithTotalDurationHours(int totalDurationHours)
    {
        _totalDurationHours = totalDurationHours;
        return this;
    }

    public ProgramOfStudyEntityBuilder WithPublishedOn(DateOnly publishedOn)
    {
        _publishedOn = publishedOn;
        return this;
    }

    public ProgramOfStudyEntityBuilder WithCompetencies(IList<CompetencyEntity> competencies)
    {
        _competencies = competencies;
        return this;
    }

    public ProgramOfStudyEntity Build()
    {
        return new ProgramOfStudyEntity
        {
            Code = _code,
            SpecificUnits = _specificUnits,
            OptionnalUnits = _optionnalUnits,
            GeneralUnits = _generalUnits,
            ComplementaryUnits = _complementaryUnits,
            Name = _name,
            ProgramType = _programType,
            MonthsDuration = _monthsDuration,
            SpecificDurationHours = _specificDurationHours,
            TotalDurationHours = _totalDurationHours,
            PublishedOn = _publishedOn,
            Competencies = _competencies
        };
    }
}
