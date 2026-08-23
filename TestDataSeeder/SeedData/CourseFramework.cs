using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Entities.CourseFramework;
using TestDataSeeder.Builders.Entities;

namespace TestDataSeeder.SeedData;

internal class CourseFramework : ISeeder<CourseFrameworkEntity>
{
    private readonly AppDbContext _context;
    private readonly ProgramOfStudyEntity _programOfStudyEntity;
    public CourseFramework(ProgramOfStudyEntity programOfStudyEntity, AppDbContext context)
    {
        _context = context;
        _programOfStudyEntity = programOfStudyEntity;
    }

    public async Task<CourseFrameworkEntity> SeedAsync()
    {
        var courseFrameworkEntity = new CourseFrameworkEntityBuilder(_programOfStudyEntity)
            .WithCode("Seeded_cf_code")
            .WithName("Seededcourseframework")
            .Build();

        await _context.CourseFrameworks.AddAsync(courseFrameworkEntity);
        await _context.SaveChangesAsync();
        return courseFrameworkEntity;
    }
}
