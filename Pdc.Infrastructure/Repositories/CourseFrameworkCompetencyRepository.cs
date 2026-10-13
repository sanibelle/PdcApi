using Microsoft.EntityFrameworkCore;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Entities.CourseFramework;

namespace Pdc.Infrastructure.Repositories;

public class CourseFrameworkCompetencyRepository(AppDbContext context) : ICourseFrameworkCompetencyRepository
{
    public async Task Link(string competencyCode, Guid courseFrameworkId)
    {
        CourseFrameworkCompetencyEntity courseFrameworkCompetencyEntity = new CourseFrameworkCompetencyEntity
        {
            CompetencyCode = competencyCode,
            CourseFrameworkId = courseFrameworkId
        };
        await context.CourseFrameworkCompetencies.AddAsync(courseFrameworkCompetencyEntity);
        await context.SaveChangesAsync();
    }

    public async Task Unlink(string competencyCode, Guid courseFrameworkId)
    {
        CourseFrameworkCompetencyEntity courseFrameworkCompetency = await context.CourseFrameworkCompetencies
            .Where(x => x.CompetencyCode == competencyCode && x.CourseFrameworkId == courseFrameworkId)
            .FirstOrDefaultAsync()
            ?? throw new NotFoundException($"Could not find CourseFrameworkCompetency when unlinking with competencyCode {competencyCode} and courseFrameworkId {courseFrameworkId}", competencyCode);

        context.CourseFrameworkCompetencies.Remove(courseFrameworkCompetency);
        await context.SaveChangesAsync();
    }
}
