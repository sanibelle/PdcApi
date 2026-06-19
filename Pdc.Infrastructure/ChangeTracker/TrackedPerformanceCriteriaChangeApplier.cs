using AutoMapper;
using Microsoft.Extensions.Logging;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.MinisterialSpecification;
using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Entities.MinisterialSpecification;

internal class TrackedPerformanceCriteriaChangeApplier : ATrackedChangeApplier<PerformanceCriteria, CompetencyElementEntity, PerformanceCriteriaEntity>
{
    public TrackedPerformanceCriteriaChangeApplier(
        AppDbContext context,
        IMapper mapper,
        IComplementaryInformationRepository complementaryInformationRepository,
        ILoggerFactory loggerFactory) : base(
            context, mapper, complementaryInformationRepository, loggerFactory)
    {
    }

    protected override void AssignParent(PerformanceCriteriaEntity entity, CompetencyElementEntity parent)
        => entity.CompetencyElement = parent;
}