using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.CourseFramework;
using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Entities.CourseFramework;

namespace Pdc.Infrastructure.Repositories;

public class CourseFrameworkRepository(AppDbContext context, IMapper mapper) : ICourseFrameworkRepository
{
    public async Task<IList<CourseFramework>> GetByProgramOfStudy(string code)
    {
        List<CourseFrameworkEntity> res = await context.CourseFrameworks
            .Where(x => x.ProgramOfStudy.Code == code)
            .ToListAsync();
        return mapper.Map<IList<CourseFramework>>(res);
    }
}
