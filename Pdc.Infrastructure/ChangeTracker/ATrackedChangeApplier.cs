using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pdc.Domain.Enums;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.Versioning;
using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Entities.Version;
using Pdc.Infrastructure.Interfaces;
using Pdc.Infrastructure.Repositories;

internal abstract class ATrackedChangeApplier<T, TParent, TEntity> : AUntrackedChangeApplier<T, TParent, TEntity>
    where T : Changeable
    where TEntity : ChangeableEntity
{
    public ATrackedChangeApplier(
        AppDbContext context,
        IMapper mapper,
        IComplementaryInformationRepository complementaryInformationRepository,
        ILoggerFactory loggerFactory) : base(
            context, mapper, complementaryInformationRepository, loggerFactory)
    {
    }


    public override async Task<TEntity> Update(ChangeRecord changeRecord, T toUpdate, IChangeTracker tracker)
    {
        if (!changeRecord.Id.HasValue)
        {
            throw new NullReferenceException($"ChangeRecordId is required for the update of {typeof(T).Name}");
        }
        TEntity changeableEntity = await Context.Set<TEntity>().SingleOrDefaultAsync(x => x.Id == toUpdate.Id) ?? throw new NotFoundException(typeof(TEntity).Name, toUpdate.Id!);
        if (changeableEntity.Value == toUpdate.Value)
        {
            return changeableEntity;
        }

        ChangeDetailEntity? changeDetail = await Context.ChangeDetails.SingleOrDefaultAsync(x => x.ChangeRecordId == changeRecord.Id.Value && x.Changeable.Id == toUpdate.Id);

        //When not traced on this version, then normal tracking is used
        if (changeDetail == null)
        {
            return await base.Update(changeRecord, toUpdate, tracker);
        }
        switch (changeDetail.ChangeType)
        {
            // The changeable will be updated, the changeDetail is intact
            case ChangeType.Update:
            case ChangeType.Add:
                changeableEntity.Value = toUpdate.Value;
                break;
            case ChangeType.Delete: // Should not be able to update a deleted change record
            default:
                throw new InvalidOperationException($"Invalid change type {changeDetail.ChangeType} for change detail with id {toUpdate.Id}");
        }
        return changeableEntity;

        // XXX TODO bug. Quand on va valider deux versions diffrentes anciennes, il faudrait aller chercher le old value de la version actuelle + n la plus près pour afficher les bonnes valeurs 
        // EX V1  V2  V3
        // A   U   D
        // Si on compare V2 et V1, on a afficher A => D et pas U car changeDetails.OldValue sera égal à A, mais changeable.Value = D. 
    }

    public override async Task Delete(List<T> updated, ICollection<TEntity> existing, IChangeTracker tracker, Guid? changeRecordId = null)
    {
        if (!changeRecordId.HasValue)
        {
            throw new NullReferenceException($"ChangeRecordId is required for deletion of {typeof(T).Name}");
        }
        (List<ChangeableEntity> changeables, List<ComplementaryInformationEntity> _) = RepoUtils.FindMissingChangeableAndComplementaryInformationsForDeletion([.. updated.Cast<Changeable>()], [.. existing.Cast<ChangeableEntity>()]);

        // removing the changeables that were added or updated on this version.
        List<ChangeDetailEntity> changeDetails = [.. Context.ChangeDetails.Where(x => x.ChangeRecordId == changeRecordId)];
        List<Guid> addedOnThisVersionIds = DeleteAddedChangeablesOnThisVersion(changeRecordId!.Value, changeables, changeDetails);
        // filters the changeables to only keep the ones not added
        changeables = [.. changeables.Where(x => !addedOnThisVersionIds.Contains(x.Id!.Value))];
        List<Guid> updatedOnThisVersionIds = DeleteUpdatedChangeablesOnThisVersion(changeables, changeDetails);
        // filters the changeables to only keep the ones not updated
        changeables = [.. changeables.Where(x => !updatedOnThisVersionIds.Contains(x.Id!.Value))];

        // the list should contain only changeables that were not updated or added on this version at this point.
        foreach (var changeable in changeables)
        {
            // if no change details were on this version, then just track the deletion
            await tracker.TrackDelete(changeable, changeable.Value, changeRecordId.Value);
        }
        await Context.SaveChangesAsync();
    }

    /// <summary>
    /// Reverts updated changeable entities that have been deleted in the current version and marks them as deleted.
    /// </summary>
    /// <remarks>This method identifies changeable entities that were updated but are now deleted in the
    /// current version. It restores their previous values and updates their change details to reflect the
    /// deletion.</remarks>
    /// <param name="changeables">The list of changeable entities to process. Each entity represents an item that may have been updated or
    /// deleted.</param>
    /// <param name="changeDetails">The list of change detail entities containing information about changes applied to the changeable entities.</param>
    /// <returns>A list of unique identifiers for the changeable entities that were reverted and marked as deleted.</returns>
    private List<Guid> DeleteUpdatedChangeablesOnThisVersion(List<ChangeableEntity> changeables, List<ChangeDetailEntity> changeDetails)
    {
        List<ChangeableEntity> moifiedChangeableToUpdate = FindChangeablesInChangeDetailsListByChangeType(changeables, ChangeType.Update, changeDetails);

        Logger.LogInformation($"Found {moifiedChangeableToUpdate.Count} changeables that were modified but deleted on this version with code with ids ${string.Join(",", moifiedChangeableToUpdate.Select(x => x.Id))}");

        foreach (var c in moifiedChangeableToUpdate)
        {
            ChangeDetailEntity changeDetail = changeDetails.Single(x => x.ChangeableId == c.Id);
            c.Value = changeDetail.OldValue!;
            changeDetail.OldValue = null;
            changeDetail.ChangeType = ChangeType.Delete;
        }
        return moifiedChangeableToUpdate.Select(x => x.Id!.Value).ToList();
    }

    /// <summary>
    /// Deletes all changeable entities that were added in the specified change record and are present in the current
    /// version.
    /// </summary>
    /// <remarks>This method removes entities that were added in the given change record and exist in the
    /// current version. The operation also removes these entities from the underlying data context. Ensure that any
    /// related entities are handled appropriately, as cascading deletes may apply depending on the data model
    /// configuration.</remarks>
    /// <param name="changeRecordId">The unique identifier of the change record to evaluate for added changeables.</param>
    /// <param name="changeables">The list of changeable entities associated with the current version.</param>
    /// <param name="changeDetails">The list of change detail entities describing changes applied in the specified change record.</param>
    /// <returns>A list of unique identifiers for the changeable entities that were deleted.</returns>
    private List<Guid> DeleteAddedChangeablesOnThisVersion(Guid changeRecordId, List<ChangeableEntity> changeables, List<ChangeDetailEntity> changeDetails)
    {
        List<ChangeableEntity> addedChangeablesToDelete = FindChangeablesInChangeDetailsListByChangeType(changeables, ChangeType.Add, changeDetails);
        Logger.LogInformation($"Found {addedChangeablesToDelete.Count} changeables that were added but deleted on this version with code with ids ${string.Join(",", addedChangeablesToDelete.Select(x => x.Id))}");
        Context.Set<TEntity>().RemoveRange(addedChangeablesToDelete.Cast<TEntity>());
        return addedChangeablesToDelete.Select(x => x.Id!.Value).ToList();
        // TODO dans mon test, valider que j'au un cascade delete sur les change details.
    }

    /// <summary>
    /// Finds all changeable entities that are associated with a specific change type in the provided list of change
    /// details. The changeDetails are already filtered by changeRecordId so no need to filter the changeables
    /// </summary>
    /// <param name="changeables">The list of changeable entities to search for associations with the specified change type.</param>
    /// <param name="changeType">The change type to match against the change details.</param>
    /// <param name="changeDetails">The list of change details used to determine which changeable entities are associated with the specified change
    /// type.</param>
    /// <returns>A list of changeable entities that are referenced by at least one change detail with the specified change type.
    /// The list will be empty if no such associations are found.</returns>
    private List<ChangeableEntity> FindChangeablesInChangeDetailsListByChangeType(List<ChangeableEntity> changeables, ChangeType changeType, List<ChangeDetailEntity> changeDetails)
    {
        return [.. changeables
            .Where(x => changeDetails
                .Where(y => y.ChangeType == changeType)
                .Any(y => y.ChangeableId == x.Id))];
    }


}