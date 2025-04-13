using Pdc.Domain.Models.Common;
using Pdc.Domain.Models.MinisterialSpecification;
using Pdc.Domain.Models.Versioning;

namespace Pdc.Tests.Builders.Models;

public class MinisterialCompetencyBuilder
{
    private string _code = "MC" + new Random().Next(1000, 9999);
    private Units? _units = new Units(5);
    private string _programOfStudyCode = "POS" + new Random().Next(1000, 9999);
    private bool _isMandatory = true;
    private bool _isOptionnal = false;
    private string _statementOfCompetency = "Default Statement";
    private List<RealisationContext> _realisationContexts = new List<RealisationContext>();
    private List<MinisterialCompetencyElement> _competencyElements = new List<MinisterialCompetencyElement>();
    private ChangeRecord _currentVersion;

    public MinisterialCompetencyBuilder() { }

    public MinisterialCompetencyBuilder WithCode(string code)
    {
        _code = code;
        return this;
    }

    public MinisterialCompetencyBuilder WithUnits(Units? units)
    {
        _units = units;
        return this;
    }

    public MinisterialCompetencyBuilder WithProgramOfStudyCode(string programOfStudyCode)
    {
        _programOfStudyCode = programOfStudyCode;
        return this;
    }

    public MinisterialCompetencyBuilder WithIsMandatory(bool isMandatory)
    {
        _isMandatory = isMandatory;
        return this;
    }

    public MinisterialCompetencyBuilder WithIsOptionnal(bool isOptionnal)
    {
        _isOptionnal = isOptionnal;
        return this;
    }

    public MinisterialCompetencyBuilder WithStatementOfCompetency(string statementOfCompetency)
    {
        _statementOfCompetency = statementOfCompetency;
        return this;
    }

    public MinisterialCompetencyBuilder WithRealisationContexts(List<RealisationContext> realisationContexts)
    {
        _realisationContexts = realisationContexts;
        return this;
    }

    public MinisterialCompetencyBuilder AddRealisationContexts(RealisationContext realisationContext)
    {
        _realisationContexts.Add(realisationContext);
        return this;
    }

    public MinisterialCompetencyBuilder WithCompetencyElements(List<MinisterialCompetencyElement> competencyElements)
    {
        _competencyElements = competencyElements;
        return this;
    }

    public MinisterialCompetencyBuilder AddCompetencyElements(MinisterialCompetencyElement competencyElement)
    {
        _competencyElements.Add(competencyElement);
        return this;
    }

    public MinisterialCompetencyBuilder WithCurrentVersion(ChangeRecord currentVersion)
    {
        _currentVersion = currentVersion;
        return this;
    }

    public MinisterialCompetency Build()
    {
        return new MinisterialCompetency
        {
            Code = _code,
            Units = _units,
            ProgramOfStudyCode = _programOfStudyCode,
            IsMandatory = _isMandatory,
            IsOptionnal = _isOptionnal,
            StatementOfCompetency = _statementOfCompetency,
            RealisationContexts = _realisationContexts,
            CompetencyElements = _competencyElements,
            CurrentVersion = _currentVersion
        };
    }
}
