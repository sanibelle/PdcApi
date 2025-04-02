using Pdc.Application.DTOS;
using Pdc.Application.DTOS.Common;
using Pdc.Domain.Models.Common;

namespace Pdc.Tests.Builders.DTOS;

public class CompetencyDTOBuilder
{
    private string _code = "DefaultCode";
    private Units? _units = null;
    private bool _isMandatory = false;
    private bool _isOptionnal = false;
    private string _statementOfCompetency = "Default Statement";
    private ICollection<ChangeableDTO> _realisationContexts = new List<ChangeableDTO>();
    private ICollection<CompetencyElementDTO> _competencyElements = new List<CompetencyElementDTO>();
    private int? _versionNumber = null;

    public CompetencyDTOBuilder WithCode(string code)
    {
        _code = code;
        return this;
    }

    public CompetencyDTOBuilder WithUnits(Units? units)
    {
        _units = units;
        return this;
    }

    public CompetencyDTOBuilder WithIsMandatory(bool isMandatory)
    {
        _isMandatory = isMandatory;
        return this;
    }

    public CompetencyDTOBuilder WithIsOptionnal(bool isOptionnal)
    {
        _isOptionnal = isOptionnal;
        return this;
    }

    public CompetencyDTOBuilder WithStatementOfCompetency(string statementOfCompetency)
    {
        _statementOfCompetency = statementOfCompetency;
        return this;
    }

    public CompetencyDTOBuilder WithRealisationContexts(ICollection<ChangeableDTO> realisationContexts)
    {
        _realisationContexts = realisationContexts;
        return this;
    }

    public CompetencyDTOBuilder WithCompetencyElements(ICollection<CompetencyElementDTO> competencyElements)
    {
        _competencyElements = competencyElements;
        return this;
    }

    public CompetencyDTOBuilder AddCompetencyElements(CompetencyElementDTO competencyElement)
    {
        _competencyElements.Add(competencyElement);
        return this;
    }

    public CompetencyDTOBuilder WithVersionNumber(int? versionNumber)
    {
        _versionNumber = versionNumber;
        return this;
    }

    public CompetencyDTO Build()
    {
        return new CompetencyDTO
        {
            Code = _code,
            Units = _units,
            IsMandatory = _isMandatory,
            IsOptionnal = _isOptionnal,
            StatementOfCompetency = _statementOfCompetency,
            RealisationContexts = _realisationContexts,
            CompetencyElements = _competencyElements,
            VersionNumber = _versionNumber
        };
    }
}
