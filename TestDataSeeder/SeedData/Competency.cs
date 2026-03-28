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

    }

    public async Task<CompetencyEntity> SeedAsync()
    {
        var user = _context.Users.First();
        var changeRecord = new ChangeRecordEntityBuilder()
            .Build();
        await _context.ChangeRecords.AddAsync(changeRecord);
        _competencyEntity = new CompetencyEntityBuilder(DataSeeder.ProgramOfStudyEntity)
            .WithCode("SEE.DED")
            .WithUnits(new UnitsEntity() { WholeUnit = 10 })
            .WithIsMandatory(false)
            .WithIsOptional(true)
            .WithStatementOfCompetency("Test Statement")
            .WithCurrentVersion(changeRecord)
            .Build();
        await _context.Competencies.AddAsync(_competencyEntity);

        var realisationContext = new RealisationContextEntityBuilder()
            .WithCompetency(_competencyEntity)
            .Build();
        await _context.RealisationContexts.AddAsync(realisationContext);
        await _context.ComplementaryInformations.AddAsync(new ComplementaryInformationEntityBuilder(realisationContext)
                .WithCreatedBy(user)
                .WithChangeRecord(changeRecord)
                .Build());

        var competencyElement = new CompetencyElementEntityBuilder()
            .WithCompetency(_competencyEntity)
            .Build();
        await _context.CompetencyElements.AddAsync(competencyElement);
        await _context.ComplementaryInformations.AddAsync(new ComplementaryInformationEntityBuilder(competencyElement)
                .WithCreatedBy(user)
                .WithChangeRecord(changeRecord)
                .Build());

        var performanceCriteria = new PerformanceCriteriaEntityBuilder()
            .WithCompetencyElement(competencyElement)
            .Build();
        await _context.PerformanceCriterias.AddAsync(performanceCriteria);
        await _context.ComplementaryInformations.AddAsync(new ComplementaryInformationEntityBuilder(performanceCriteria)
                .WithCreatedBy(user)
                .WithChangeRecord(changeRecord)
                .Build());
        await _context.SaveChangesAsync();
        return _competencyEntity;
    }
}
