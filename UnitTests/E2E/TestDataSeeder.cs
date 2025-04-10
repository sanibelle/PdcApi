using Microsoft.EntityFrameworkCore;
using Pdc.Domain.Enums;
using Pdc.Domain.Models.Common;
using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Entities.CourseFramework;
using Pdc.Infrastructure.Entities.MinisterialSpecification;
using Pdc.Tests.Builders.Entities;

namespace Pdc.E2ETests;

[TestFixture]

// Test data seeder service
public class TestDataSeeder
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public static ProgramOfStudyEntity ProgramOfStudyEntity { get; set; }
    public static CompetencyEntity CompetencyEntity { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private readonly AppDbContext _context;

    public TestDataSeeder(AppDbContext context)
    {
        _context = context;
        CreateProgramOfStudy();
        CreateCompetency();
    }

    private void CreateProgramOfStudy()
    {
        ProgramOfStudyEntity = new ProgramOfStudyEntityBuilder()
            .WithCode("Test.123")
            .WithName("Test Program of Study")
            .WithSanction(SanctionType.DEC)
            .WithMonthsDuration(24)
            .WithSpecificDurationHours(1800)
            .WithTotalDurationHours(4500)
            .WithPublishedOn(DateOnly.FromDateTime(DateTime.Now))
            .WithCompetencies(new List<CompetencyEntity>())
            .WithOptionnalUnits(new Units(10, 1, 2))
            .WithSpecificUnits(new Units(60))
            .Build();
    }

    private void CreateCompetency()
    {
        var changeRecord = new ChangeRecordEntityBuilder()
            .Build();

        var realisationContext = new RealisationContextEntityBuilder()
            .AddComplementaryInformations(new ComplementaryInformationEntityBuilder()
                .WithChangeRecord(changeRecord)
                .Build())
            .Build();

        var performanceCriteria = new PerformanceCriteriaEntityBuilder()
            .AddComplementaryInformation(new ComplementaryInformationEntityBuilder()
                .WithChangeRecord(changeRecord)
                .Build())
            .Build();

        var competencyElement = new CompetencyElementEntityBuilder()
            .AddPerformanceCriteria(performanceCriteria)
            .AddComplementaryInformation(new ComplementaryInformationEntityBuilder()
                .WithChangeRecord(changeRecord)
                .Build())
            .Build();

        CompetencyEntity = new CompetencyEntityBuilder()
            .WithCode("SEE.DED")
            .WithUnits(new Units(10))
            .WithIsMandatory(false)
            .WithIsOptionnal(true)
            .WithStatementOfCompetency("Test Statement")
            .AddRealisationContexts(realisationContext)
            .AddCompetencyElements(competencyElement)
            .WithCurrentVersion(changeRecord)
            .WithProgramOfStudy(ProgramOfStudyEntity)
            .Build();
    }


    public async Task SeedTestData()
    {
        await _context.Database.EnsureCreatedAsync();

        if (!await _context.ProgramOfStudies.AnyAsync())
        {
            _context.ProgramOfStudies.Add(ProgramOfStudyEntity);
            _context.Competencies.Add(CompetencyEntity);

            await _context.SaveChangesAsync();
        }
    }
}
