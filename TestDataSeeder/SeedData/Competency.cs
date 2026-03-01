using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Entities.CourseFramework;
using Pdc.Infrastructure.Entities.MinisterialSpecification;
using TestDataSeeder.Builders.Entities;
namespace TestDataSeeder.SeedData;

internal class Competency : ISeeder<CompetencyEntity>
{
    private CompetencyEntity _competencyEntity;
    private AppDbContext _context;
    public Competency(ProgramOfStudyEntity programOfStudyEntity, AppDbContext context)
    {
        _context  = context;
        var user = _context.Users.First();
        var changeRecord = new ChangeRecordEntityBuilder()
            .Build();

        var realisationContext = new RealisationContextEntityBuilder()
            .AddComplementaryInformations(new ComplementaryInformationEntityBuilder()
        .WithChangeRecord(changeRecord)
                .WithCreatedBy(user)
                .Build())
            .Build();

        var performanceCriteria = new PerformanceCriteriaEntityBuilder()
            .AddComplementaryInformation(new ComplementaryInformationEntityBuilder()
                .WithCreatedBy(user)
                .WithChangeRecord(changeRecord)
                .Build())
            .Build();

        var competencyElement = new CompetencyElementEntityBuilder()
            .AddPerformanceCriteria(performanceCriteria)
            .AddComplementaryInformation(new ComplementaryInformationEntityBuilder()
                .WithCreatedBy(user)
                .WithChangeRecord(changeRecord)
                .Build())
            .Build();

        _competencyEntity = new CompetencyEntityBuilder()
            .WithCode("SEE.DED")
            .WithUnits(new UnitsEntity() { WholeUnit = 10 })
            .WithIsMandatory(false)
            .WithIsOptional(true)
            .WithStatementOfCompetency("Test Statement")
            .AddRealisationContexts(realisationContext)
            .AddCompetencyElements(competencyElement)
            .WithCurrentVersion(changeRecord)
            .WithProgramOfStudy(programOfStudyEntity)
            .Build();
    }

    public async Task<CompetencyEntity> SeedAsync()
    {
        await _context.Competencies.AddAsync(_competencyEntity);
        await _context.SaveChangesAsync();
        return _competencyEntity;
    }
}
