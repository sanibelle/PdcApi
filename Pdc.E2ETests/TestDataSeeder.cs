using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Pdc.Domain.Entities.CourseFramework;
using Pdc.Domain.Enums;
using Pdc.Infrastructure.Data;
using System;
using System.Threading.Tasks;

namespace Pdc.E2ETests;

[TestFixture]

// Test data seeder service
public class TestDataSeeder
{
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
            _context.ProgramOfStudies.Add(new ProgramOfStudy
            {
                Code = "TEST.123",
                Name = "Test Program of Study",
                Sanction = SanctionType.DEC,
                MonthsDuration = 24,
                SpecificDurationHours = 1800,
                TotalDurationHours = 4500,
                PublishedOn = DateOnly.FromDateTime(DateTime.Now),
                Competencies = []
            });

            await _context.SaveChangesAsync();
        }
    }
}
