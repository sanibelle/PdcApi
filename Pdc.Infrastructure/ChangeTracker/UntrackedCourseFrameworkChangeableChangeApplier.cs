using AutoMapper;
using Microsoft.Extensions.Logging;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.Versioning;
using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Entities.CourseFramework;
using Pdc.Infrastructure.Entities.Version;

internal class UntrackedCourseFrameworkChangeableChangeApplier(
    AppDbContext context,
    IMapper mapper,
    IComplementaryInformationRepository complementaryInformationRepository,
    ILoggerFactory loggerFactory)
    : AUntrackedChangeApplier<Changeable, CourseFrameworkEntity, CourseFrameworkChangeableEntity>(
        context, mapper, complementaryInformationRepository, loggerFactory)
{
    protected override void AssignParent(CourseFrameworkChangeableEntity entity, CourseFrameworkEntity parent)
    {
        entity.CourseFramework = parent;
    }
}