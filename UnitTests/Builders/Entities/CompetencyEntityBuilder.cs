using Pdc.Infrastructure.Entities.MinisterialSpecification;

public class CompetencyEntityBuilder
{
    private string _code = "00SU";
    private Units? _units = new Units(3);
    private ProgramOfStudyEntity _programOfStudy = new ProgramOfStudyEntityBuilder().Build();
    private bool _isMandatory = true;
    private bool _isOptionnal = false;
    private string _statementOfCompetency = "Effectuer le déploiement de serveurs intranet";
    private IList<RealisationContextEntity> _realisationContexts = new List<RealisationContextEntity>();
    private IList<CompetencyElementEntity> _competencyElements = new List<CompetencyElementEntity>();

    public CompetencyEntityBuilder WithCode(string code)
    {
        _code = code;
        return this;
    }

    public CompetencyEntityBuilder WithUnits(Units units)
    {
        _units = units;
        return this;
    }

    public CompetencyEntityBuilder WithProgramOfStudy(ProgramOfStudyEntity programOfStudy)
    {
        _programOfStudy = programOfStudy;
        return this;
    }

    public CompetencyEntityBuilder WithIsMandatory(bool isMandatory)
    {
        _isMandatory = isMandatory;
        return this;
    }

    public CompetencyEntityBuilder WithIsOptionnal(bool isOptionnal)
    {
        _isOptionnal = isOptionnal;
        return this;
    }

    public CompetencyEntityBuilder WithStatementOfCompetency(string statementOfCompetency)
    {
        _statementOfCompetency = statementOfCompetency;
        return this;
    }

    public CompetencyEntityBuilder WithRealisationContexts(IList<RealisationContextEntity> realisationContexts)
    {
        _realisationContexts = realisationContexts;
        return this;
    }

    public CompetencyEntityBuilder WithCompetencyElements(IList<CompetencyElementEntity> competencyElements)
    {
        _competencyElements = competencyElements;
        return this;
    }

    public CompetencyEntity Build()
    {
        return new CompetencyEntity
        {
            Code = _code,
            Units = _units,
            ProgramOfStudy = _programOfStudy,
            IsMandatory = _isMandatory,
            IsOptionnal = _isOptionnal,
            StatementOfCompetency = _statementOfCompetency,
            RealisationContexts = _realisationContexts,
            CompetencyElements = _competencyElements
        };
    }
}
