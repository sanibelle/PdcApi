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
            throw new NullReferenceException($"ChangeRecordId is required for deletion of {typeof(T).Name}");
        }
        ChangeDetailEntity? changeDetail = await Context.ChangeDetails.SingleOrDefaultAsync(x => x.ChangeRecordId == changeRecord.Id.Value && x.Changeable.Id == toUpdate.Id);

        //When not traced on this version, then normal tracking is used
        if (changeDetail == null)
        {
            return await base.Update(changeRecord, toUpdate, tracker);
        }
        TEntity changeableEntity = await Context.Set<TEntity>().SingleOrDefaultAsync(x => x.Id == toUpdate.Id) ?? throw new NotFoundException(typeof(TEntity).Name, toUpdate.Id!);
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

        // TODO bug. Quand on va valider deux versions diffrentes anciennes, il faudrait aller chercher le old value de la version actuelle + n la plus près pour afficher les bonnes valeurs 
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
        List<Guid> addedOnThisVersionIds = AddedOnThisVersion(changeRecordId!.Value, changeables, changeDetails);
        changeables = [.. changeables.Where(x => !addedOnThisVersionIds.Contains(x.Id!.Value))];
        List<Guid> updatedOnThisVersionIds = UpdatedOnThisVersion(changeRecordId!.Value, changeables, changeDetails);
        changeables = [.. changeables.Where(x => !updatedOnThisVersionIds.Contains(x.Id!.Value))];

        // if no change details were on this version, then just track the deletion
        foreach (var changeable in changeables)
        {
            await tracker.TrackDelete(changeable, changeable.Value, changeRecordId.Value);
        }
        await Context.SaveChangesAsync();
    }

    private List<Guid> UpdatedOnThisVersion(Guid? changeRecordId, List<ChangeableEntity> changeables, List<ChangeDetailEntity> changeDetails)
    {
        List<ChangeableEntity> moifiedChangeableToUpdate = GetChangeablesInChangeDetailsList(changeRecordId, changeables, ChangeType.Update, changeDetails);
        List<ChangeDetailEntity> changeDetailsToUpdate = GetChangeDetailsLinkedToChangeables(changeRecordId, moifiedChangeableToUpdate, changeDetails);

        Logger.LogInformation($"Found {moifiedChangeableToUpdate.Count} changeables that were moified but deleted on this version with code with ids ${string.Join(",", moifiedChangeableToUpdate.Select(x => x.Id))}");
        Logger.LogInformation($"Found {changeDetailsToUpdate.Count} change details that were moified but deleted on this version with code with ids ${string.Join(",", moifiedChangeableToUpdate.Select(x => x.Id))}");

        if (moifiedChangeableToUpdate.Count != changeDetailsToUpdate.Count)
        {
            throw new InvalidOperationException("The count of the changeables an the change details should be the same but it is not");
        }
        foreach (var c in moifiedChangeableToUpdate)
        {
            ChangeDetailEntity changeDetail = changeDetailsToUpdate.Single(x => x.Changeable.Id == c.Id);
            c.Value = changeDetail.OldValue!;
            changeDetail.OldValue = null;
            changeDetail.ChangeType = ChangeType.Delete;
        }
        return moifiedChangeableToUpdate.Select(x => x.Id!.Value).ToList();
    }

    private List<Guid> AddedOnThisVersion(Guid? changeRecordId, List<ChangeableEntity> changeables, List<ChangeDetailEntity> changeDetails)
    {
        List<ChangeableEntity> addedChangeableToDelete = GetChangeablesInChangeDetailsList(changeRecordId, changeables, ChangeType.Add, changeDetails);
        List<ChangeDetailEntity> changeDetailsToDelete = GetChangeDetailsLinkedToChangeables(changeRecordId, addedChangeableToDelete, changeDetails);

        Logger.LogInformation($"Found {addedChangeableToDelete.Count} changeables that were added but deleted on this version with code with ids ${string.Join(",", addedChangeableToDelete.Select(x => x.Id))}");
        Logger.LogInformation($"Found {changeDetailsToDelete.Count} change details that were added but deleted on this version with code with ids ${string.Join(",", addedChangeableToDelete.Select(x => x.Id))}");

        if (addedChangeableToDelete.Count != changeDetailsToDelete.Count)
        {
            throw new InvalidOperationException("The count of the changeables an the change details should be the same but it is not");
        }
        Context.Set<TEntity>().RemoveRange(addedChangeableToDelete.Cast<TEntity>());
        Context.ChangeDetails.RemoveRange(changeDetailsToDelete);
        return addedChangeableToDelete.Select(x => x.Id!.Value).ToList();
    }

    private List<ChangeDetailEntity> GetChangeDetailsLinkedToChangeables(Guid? changeRecordId, List<ChangeableEntity> addedChangeableToDelete, List<ChangeDetailEntity> changeDetails)
    {
        return [.. changeDetails
                    .Where(x => x.ChangeRecordId == changeRecordId)
                    .Where(x => addedChangeableToDelete
                        .Any(y => y.Id == x.Changeable.Id))];
    }

    private List<ChangeableEntity> GetChangeablesInChangeDetailsList(Guid? changeRecordId, List<ChangeableEntity> changeables, ChangeType changeType, List<ChangeDetailEntity> changeDetails)
    {
        return [.. changeables
            .Where(x => changeDetails
                .Where(y => y.ChangeType == changeType && y.ChangeRecordId == changeRecordId)
                .Any(y => y.Changeable.Id == x.Id))];
    }


}