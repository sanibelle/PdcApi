using Pdc.Application.DTOS;
using Pdc.Domain.Entities.Common;
using Pdc.Domain.Entities.MinisterialSpecification;

namespace Pdc.Tests.Builders.DTOS;

public class ProgramOfStudyDTOBuilder : CreateProgramOfStudyDTOBuilder
{
    private List<MinisterialCompetency> _competencies;
    private Units _generalUnits = new Units(1);
    private Units _complementaryUnits = new Units(2);

    public ProgramOfStudyDTOBuilder()
    {
        _competencies = new List<MinisterialCompetency>();
    }

    private ProgramOfStudyDTOBuilder(CreateProgramOfStudyDTO createProgramOfStudyDTO)
    {
        _competencies = new List<MinisterialCompetency>();
        _code = createProgramOfStudyDTO.Code;
        _name = createProgramOfStudyDTO.Name;
        _sanction = createProgramOfStudyDTO.Sanction;
        _monthsDuration = createProgramOfStudyDTO.MonthsDuration;
        _specificDurationHours = createProgramOfStudyDTO.SpecificDurationHours;
        _totalDurationHours = createProgramOfStudyDTO.TotalDurationHours;
        _publishedOn = createProgramOfStudyDTO.PublishedOn;
        _specificUnits = createProgramOfStudyDTO.SpecificUnits;
        _optionnalUnits = createProgramOfStudyDTO.OptionnalUnits;
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

    public override ProgramOfStudyDTO Build()
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
            ComplementaryUnits = _complementaryUnits
        };
    }
}
