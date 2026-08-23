using AutoMapper;
using Microsoft.Extensions.Logging;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.Versioning;
using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Entities.CourseFramework;
using Pdc.Infrastructure.Entities.Version;

internal class TrackedCourseFrameworkChangeableChangeApplier(
    AppDbContext context,
    IMapper mapper,
    IComplementaryInformationRepository complementaryInformationRepository,
    ILoggerFactory loggerFactory)
    : ATrackedChangeApplier<Changeable, CourseFrameworkEntity, CourseFrameworkChangeableEntity>(
        context, mapper, complementaryInformationRepository, loggerFactory)
{
    protected override void AssignParent(CourseFrameworkChangeableEntity entity, CourseFrameworkEntity parent)
    {
        entity.CourseFramework = parent;
    }
}