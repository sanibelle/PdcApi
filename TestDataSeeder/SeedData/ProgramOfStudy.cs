using Pdc.Domain.Enums;
using Pdc.Domain.Models.Common;
using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Entities.CourseFramework;
using Pdc.Infrastructure.Entities.MinisterialSpecification;
using TestDataSeeder.Builders.Entities;

namespace TestDataSeeder.SeedData;

internal class ProgramOfStudy : ISeeder<ProgramOfStudyEntity>
{
    private AppDbContext _context;
    public ProgramOfStudy(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ProgramOfStudyEntity> SeedAsync()
    {
        var programOfStudyEntity = new ProgramOfStudyEntityBuilder()
            .WithCode("Seededprogram")
            .WithName("Test Program of Study")
            .WithProgramType(ProgramType.DEC)
            .WithMonthsDuration(24)
            .WithSpecificDurationHours(1800)
            .WithTotalDurationHours(4500)
            .WithPublishedOn(DateOnly.FromDateTime(DateTime.Now))
            .WithCompetencies(new List<CompetencyEntity>())
            .WithOptionalUnits(new Units(10, 1, 2))
            .WithSpecificUnits(new Units(60))
            .Build();
        await _context.ProgramOfStudies.AddAsync(programOfStudyEntity);
        await _context.SaveChangesAsync();
        return programOfStudyEntity;
    }
}
