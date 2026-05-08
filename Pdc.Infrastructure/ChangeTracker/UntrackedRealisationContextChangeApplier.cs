using AutoMapper;
using Microsoft.Extensions.Logging;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.Common;
using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Entities.MinisterialSpecification;

internal class UntrackedRealisationContextChangeApplier(
    AppDbContext context,
    IMapper mapper,
    IComplementaryInformationRepository complementaryInformationRepository,
    ILoggerFactory loggerFactory)
    : AUntrackedChangeApplier<RealisationContext, CompetencyEntity, RealisationContextEntity>(
        context, mapper, complementaryInformationRepository, loggerFactory)
{
    protected override void AssignParent(RealisationContextEntity entity, CompetencyEntity parent)
    {
        entity.Competency = parent;
    }
}