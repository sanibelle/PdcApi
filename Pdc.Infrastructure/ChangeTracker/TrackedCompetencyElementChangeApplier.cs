using AutoMapper;
using Microsoft.Extensions.Logging;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.MinisterialSpecification;
using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Entities.MinisterialSpecification;

internal class TrackedCompetencyElementChangeApplier : ATrackedChangeApplier<MinisterialCompetencyElement, CompetencyEntity, CompetencyElementEntity>
{
    public TrackedCompetencyElementChangeApplier(
        AppDbContext context,
        IMapper mapper,
        IComplementaryInformationRepository complementaryInformationRepository,
        ILoggerFactory loggerFactory) : base(
            context, mapper, complementaryInformationRepository, loggerFactory)
    {
    }

    protected override void AssignParent(CompetencyElementEntity entity, CompetencyEntity parent)
        => entity.Competency = parent;
}