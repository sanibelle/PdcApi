using Microsoft.EntityFrameworkCore;
using Pdc.Domain.Models.Versioning;
using Pdc.Infrastructure.Entities.Versioning;

namespace Pdc.Infrastructure.Repositories;

internal static class RepoUtils
{
    static internal (List<ChangeableEntity>, List<ComplementaryInformationEntity>) FindMissingAChangeableAndComplementaryInformationsForDeletion(List<AChangeable> listWithMissing, List<ChangeableEntity> listToCompare)
    {
        var changeableToDelete = listToCompare
            .Where(x => !listWithMissing
                .Select(y => y.Id).Contains(x.Id))
            .ToList();

        List<ComplementaryInformationEntity> complementaryInformationsToRemove = listToCompare.SelectMany(x => x.ComplementaryInformations)
            .Where(x => !listWithMissing.SelectMany(x => x.ComplementaryInformations).Any(y => y.Id == x.Id))
            .ToList();

        // Not needed, cascade delete should do the trick.
        // complementaryInformationsToRemove.AddRange(changeableToDelete.SelectMany(x => x.ComplementaryInformations).ToList());
        return (changeableToDelete, complementaryInformationsToRemove);
    }
}
