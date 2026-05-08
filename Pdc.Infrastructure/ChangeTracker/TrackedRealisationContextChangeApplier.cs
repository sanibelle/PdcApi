using AutoMapper;
using Microsoft.Extensions.Logging;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.Common;
using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Entities.MinisterialSpecification;

internal class TrackedRealisationContextChangeApplier : ATrackedChangeApplier<RealisationContext, CompetencyEntity, RealisationContextEntity>
{
    public TrackedRealisationContextChangeApplier(
        AppDbContext context,
        IMapper mapper,
        IComplementaryInformationRepository complementaryInformationRepository,
        ILoggerFactory loggerFactory) : base(context, mapper, complementaryInformationRepository, loggerFactory)
    {
    }

    protected override void AssignParent(RealisationContextEntity entity, CompetencyEntity parent)
    {
        entity.Competency = parent;
    }
}