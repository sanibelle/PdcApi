using Pdc.Infrastructure.Entities.Version;
using Pdc.Infrastructure.Entities.CourseFramework;
using Pdc.Infrastructure.Entities.MinisterialSpecification;

namespace TestDataSeeder.Builders.Entities;

public class CompetencyEntityBuilder(ProgramOfStudyEntity programOfStudy)
{
    private string _code = "00SU";
    private UnitsEntity? _units = new UnitsEntityBuilder().WithWholeUnit(3).Build();
    private ProgramOfStudyEntity _programOfStudy = programOfStudy;
    private bool _isMandatory = true;
    private bool _isOptional = false;
    private string _statementOfCompetency = "Effectuer le déploiement de serveurs intranet";
    private IList<RealisationContextEntity> _realisationContexts = new List<RealisationContextEntity>();
    private IList<CompetencyElementEntity> _competencyElements = new List<CompetencyElementEntity>();
    private ChangeRecordEntity? _changeRecord;

    public CompetencyEntityBuilder WithCode(string code)
    {
        _code = code;
        return this;
    }

    public CompetencyEntityBuilder WithUnits(UnitsEntity units)
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

    public CompetencyEntityBuilder WithIsOptional(bool isOptional)
    {
        _isOptional = isOptional;
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

    public CompetencyEntityBuilder AddRealisationContexts(RealisationContextEntity realisationContexts)
    {
        _realisationContexts.Add(realisationContexts);
        return this;
    }

    public CompetencyEntityBuilder AddCompetencyElements(CompetencyElementEntity competencyElements)
    {
        _competencyElements.Add(competencyElements);
        return this;
    }

    public CompetencyEntityBuilder WithCurrentChangeRecord(ChangeRecordEntity changeRecord)
    {
        _changeRecord = changeRecord;
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
            IsOptional = _isOptional,
            StatementOfCompetency = _statementOfCompetency,
            RealisationContexts = _realisationContexts,
            CompetencyElements = _competencyElements,
            ChangeRecord = _changeRecord
        };
    }
}
