using Pdc.Application.DTOS;
using Pdc.Domain.Enums;
using Pdc.Domain.Models.Common;
using Pdc.Domain.Models.MinisterialSpecification;

namespace Pdc.Tests.Builders.DTOS;

public class ProgramOfStudyDTOBuilder
{
    private List<MinisterialCompetency> _competencies = new List<MinisterialCompetency>();
    private Units _generalUnits = new Units(1);
    private Units _complementaryUnits = new Units(2);
    private string _code = $"TES ${Random.Shared.Next(100, 1000)}";
    private string _name = "Default Test Program Of Study";
    private SanctionType _sanction = SanctionType.DEC;
    private int _monthsDuration = 30;
    private int _specificDurationHours = 540;
    private int _totalDurationHours = 1340;
    private DateOnly _publishedOn = new DateOnly(2025, 03, 31);
    private Units _specificUnits = new Units(3);
    private Units _optionnalUnits = new Units(4);
    public ICollection<CompetencyDTO> _competencyDTOs { get; set; } = new List<CompetencyDTO>();

    public ProgramOfStudyDTOBuilder()
    {
    }

    public ProgramOfStudyDTOBuilder WithCompetencies(List<MinisterialCompetency> competencies)
    {
        _competencies = competencies;
        return this;
    }

    public ProgramOfStudyDTOBuilder WithGeneralUnits(Units generalUnits)
    {
        _generalUnits = generalUnits;
        return this;
    }

    public ProgramOfStudyDTOBuilder WithComplementaryUnits(Units complementaryUnits)
    {
        _complementaryUnits = complementaryUnits;
        return this;
    }

    public ProgramOfStudyDTOBuilder WithCode(string code)
    {
        _code = code;
        return this;
    }

    public ProgramOfStudyDTOBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public ProgramOfStudyDTOBuilder WithSanction(SanctionType sanction)
    {
        _sanction = sanction;
        return this;
    }

    public ProgramOfStudyDTOBuilder WithMonthsDuration(int monthsDuration)
    {
        _monthsDuration = monthsDuration;
        return this;
    }

    public ProgramOfStudyDTOBuilder WithSpecificDurationHours(int specificDurationHours)
    {
        _specificDurationHours = specificDurationHours;
        return this;
    }

    public ProgramOfStudyDTOBuilder WithTotalDurationHours(int totalDurationHours)
    {
        _totalDurationHours = totalDurationHours;
        return this;
    }

    public ProgramOfStudyDTOBuilder WithPublishedOn(DateOnly publishedOn)
    {
        _publishedOn = publishedOn;
        return this;
    }

    public ProgramOfStudyDTOBuilder WithSpecificUnits(Units specificUnits)
    {
        _specificUnits = specificUnits;
        return this;
    }

    public ProgramOfStudyDTOBuilder WithOptionnalUnits(Units optionnalUnits)
    {
        _optionnalUnits = optionnalUnits;
        return this;
    }

    public ProgramOfStudyDTOBuilder WithCompetencies(ICollection<CompetencyDTO> competencyDTOs)
    {
        _competencyDTOs = competencyDTOs;
        return this;
    }

    public ProgramOfStudyDTO Build()
    {
        return new ProgramOfStudyDTO
        {
            Code = _code,
            Name = _name,
            Sanction = _sanction,
            MonthsDuration = _monthsDuration,
            SpecificDurationHours = _specificDurationHours,
            TotalDurationHours = _totalDurationHours,
            PublishedOn = _publishedOn,
            SpecificUnits = _specificUnits,
            OptionnalUnits = _optionnalUnits,
            GeneralUnits = _generalUnits,
            ComplementaryUnits = _complementaryUnits,
            CompetencyDTOs = _competencyDTOs
        };
    }
}
