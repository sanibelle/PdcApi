using Microsoft.EntityFrameworkCore;
using Pdc.Domain.Enums;
using Pdc.Domain.Models.Common;
using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Entities.CourseFramework;
using Pdc.Infrastructure.Entities.MinisterialSpecification;

namespace Pdc.E2ETests;

[TestFixture]

// Test data seeder service
public class TestDataSeeder
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public static ProgramOfStudyEntity ProgramOfStudyEntity { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private readonly AppDbContext _context;

    public TestDataSeeder(AppDbContext context)
    {
        _context = context;
        CreateEntities();
    }

    private void CreateEntities()
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

    public async Task SeedTestData()
    {
        await _context.Database.EnsureCreatedAsync();

        if (!await _context.ProgramOfStudies.AnyAsync())
        {
            _context.ProgramOfStudies.Add(ProgramOfStudyEntity);

            await _context.SaveChangesAsync();
        }
    }
}
