using Microsoft.EntityFrameworkCore;
using Pdc.Domain.Models.Versioning;
using Pdc.Infrastructure.Entities.Version;

namespace Pdc.Infrastructure.Repositories;

internal static class RepoUtils
{
    static internal (List<ChangeableEntity>, List<ComplementaryInformationEntity>) FindMissingChangeableAndComplementaryInformationsForDeletion(List<Changeable> listWithMissing, List<ChangeableEntity> listToCompare)
    {
        var changeableToDelete = listToCompare
            .Where(x => !listWithMissing
                .Select(y => y.Id).Contains(x.Id))
            .ToList();

        // Gets all the complementary informations to delete that are not handled  by the cascade delete.
        // i.e. I deleted the complementary information of a realisation context but the realisation context is still present
        List<ComplementaryInformationEntity> complementaryInformationsToRemove = listToCompare.SelectMany(x => x.ComplementaryInformations!)
            .Where(x => !listWithMissing.SelectMany(x => x.ComplementaryInformations).Any(y => y.Id == x.Id))
            .ToList();

        return (changeableToDelete, complementaryInformationsToRemove);
    }
}
