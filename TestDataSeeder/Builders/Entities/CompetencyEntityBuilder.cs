using Pdc.Domain.Models.Common;
using Pdc.Infrastructure.Entities.CourseFramework;
using Pdc.Infrastructure.Entities.MinisterialSpecification;
using Pdc.Infrastructure.Entities.Versioning;

namespace TestDataSeeder.Builders.Entities;

public class CompetencyEntityBuilder
{
    private string _code = "00SU";
    private Units? _units = new Units(3);
    private ProgramOfStudyEntity? _programOfStudy;
    private bool _isMandatory = true;
    private bool _isOptional = false;
    private string _statementOfCompetency = "Effectuer le déploiement de serveurs intranet";
    private IList<RealisationContextEntity> _realisationContexts = new List<RealisationContextEntity>();
    private IList<CompetencyElementEntity> _competencyElements = new List<CompetencyElementEntity>();
    private ChangeRecordEntity? _currentVersion;

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

    public CompetencyEntityBuilder WithCurrentVersion(ChangeRecordEntity currentVersion)
    {
        _currentVersion = currentVersion;
        return this;
    }

    public CompetencyEntity Build()
    {
        return new CompetencyEntity
        {
            Code = _code,
            Units = _units,
            ProgramOfStudy = _programOfStudy ??  new ProgramOfStudyEntityBuilder().Build(),
            IsMandatory = _isMandatory,
            IsOptional = _isOptional,
            StatementOfCompetency = _statementOfCompetency,
            RealisationContexts = _realisationContexts,
            CompetencyElements = _competencyElements,
            CurrentVersion = _currentVersion
        };
    }
}
