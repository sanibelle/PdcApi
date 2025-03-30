using Microsoft.EntityFrameworkCore;
using Pdc.Domain.Entities.Common;
using Pdc.Domain.Enums;
using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Entities.CourseFramework;
using Pdc.Infrastructure.Entities.MinisterialSpecification;

namespace Pdc.E2ETests;

[TestFixture]

// Test data seeder service
public class TestDataSeeder
{
    public static ProgramOfStudyEntity ProgramOfStudyEntity { get; set; } = null;
    private readonly AppDbContext _context;

    public TestDataSeeder(AppDbContext context)
    {
        _context = context;
    }

    public async Task SeedTestData()
    {
        await _context.Database.EnsureCreatedAsync();

        if (!await _context.ProgramOfStudies.AnyAsync())
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

            _context.ProgramOfStudies.Add(ProgramOfStudyEntity);

            await _context.SaveChangesAsync();
        }
    }
}
