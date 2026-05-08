
using AutoMapper;
using Microsoft.Extensions.Logging;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.MinisterialSpecification;
using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Entities.MinisterialSpecification;

internal class UntrackedPerformanceCriteriaChangeApplier(
    AppDbContext context,
    IMapper mapper,
    IComplementaryInformationRepository complementaryInformationRepository,
    ILoggerFactory loggerFactory)
    : AUntrackedChangeApplier<PerformanceCriteria, CompetencyElementEntity, PerformanceCriteriaEntity>(
        context, mapper, complementaryInformationRepository, loggerFactory)
{
    protected override void AssignParent(PerformanceCriteriaEntity entity, CompetencyElementEntity parent)
        => entity.CompetencyElement = parent;
}
