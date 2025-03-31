using Pdc.Application.DTOS;

namespace Pdc.Tests.Builders.DTOS;

public class CreateCompetencyDTOBuilder
{
    protected string _code = "COMP" + new Random().Next(1000, 9999);
    protected bool _isMandatory = true;
    protected bool _isOptionnal = false;
    protected string _statementOfCompetency = "Default Statement of Competency";
    protected Units? _units = null;
    protected IEnumerable<CreateChangeableDTO> _realisationContexts = new List<CreateChangeableDTO>();
    protected IEnumerable<CreateCompetencyElementDTO> _competencyElements = new List<CreateCompetencyElementDTO>();

    public CreateCompetencyDTOBuilder()
    {
    }

    public CreateCompetencyDTOBuilder WithCode(string code)
    {
        _code = code;
        return this;
    }

    public CreateCompetencyDTOBuilder WithIsMandatory(bool isMandatory)
    {
        _isMandatory = isMandatory;
        return this;
    }

    public CreateCompetencyDTOBuilder WithIsOptionnal(bool isOptionnal)
    {
        _isOptionnal = isOptionnal;
        return this;
    }

    public CreateCompetencyDTOBuilder WithStatementOfCompetency(string statementOfCompetency)
    {
        _statementOfCompetency = statementOfCompetency;
        return this;
    }

    public CreateCompetencyDTOBuilder WithUnits(Units? units)
    {
        _units = units;
        return this;
    }

    public CreateCompetencyDTOBuilder WithRealisationContexts(IEnumerable<CreateChangeableDTO> realisationContexts)
    {
        _realisationContexts = realisationContexts;
        return this;
    }

    public CreateCompetencyDTOBuilder WithCompetencyElements(IEnumerable<CreateCompetencyElementDTO> competencyElements)
    {
        _competencyElements = competencyElements;
        return this;
    }

    public virtual CreateCompetencyDTO Build()
    {
        return new CreateCompetencyDTO
        {
            Code = _code,
            IsMandatory = _isMandatory,
            IsOptionnal = _isOptionnal,
            StatementOfCompetency = _statementOfCompetency,
            Units = _units,
            RealisationContexts = _realisationContexts,
            CompetencyElements = _competencyElements
        };
    }
}
