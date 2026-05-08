using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.Versioning;
using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Entities.Version;
using Pdc.Infrastructure.Interfaces;
using Pdc.Infrastructure.Repositories;

internal abstract class AUntrackedChangeApplier<T, TParent, TEntity> : IChangeApplier<T, TParent, TEntity>
    where T : Changeable
    where TEntity : ChangeableEntity
{
    protected AppDbContext Context;
    protected IMapper Mapper;
    protected ILogger Logger;
    private IComplementaryInformationRepository _complementaryInformationRepository;
    public AUntrackedChangeApplier(AppDbContext context, IMapper mapper, IComplementaryInformationRepository complementaryInformationRepository, ILoggerFactory loggerFactory)
    {
        Context = context;
        Mapper = mapper;
        _complementaryInformationRepository = complementaryInformationRepository;
        Logger = loggerFactory.CreateLogger(GetType());
    }

    protected abstract void AssignParent(TEntity entity, TParent parent);

    public virtual async Task<TEntity> Add(
        ChangeRecord changeRecord, TParent parentEntity, T toAdd, IChangeTracker tracker)
    {
        Logger.LogInformation($"Adding {typeof(TEntity).Name} with value {toAdd.Value}");

        TEntity entity = Mapper.Map<TEntity>(toAdd);
        AssignParent(entity, parentEntity);

        EntityEntry<TEntity> added = await Context.Set<TEntity>().AddAsync(entity);
        await Context.SaveChangesAsync();

        await UpsertComplementaryInformations(
            changeRecord.Id!.Value, toAdd.ComplementaryInformations, added.Entity.Id!.Value);
        await tracker.TrackAdd(added.Entity, changeRecord.Id.Value);

        return added.Entity;
    }

    public virtual async Task<TEntity> Update(
        ChangeRecord changeRecord, T toUpdate, IChangeTracker tracker)
    {
        Logger.LogInformation($"Updating {typeof(TEntity).Name} with id {toUpdate.Id} with value {toUpdate.Value}");

        TEntity entityToUpdate = await Context.Set<TEntity>()
            .Include(x => x.ComplementaryInformations)
            .SingleOrDefaultAsync(x => x.Id == toUpdate.Id)
            ?? throw new NotFoundException(typeof(TEntity).Name, toUpdate.Id!);

        string oldValue = entityToUpdate.Value;

        Mapper.Map(toUpdate, entityToUpdate);
        EntityEntry<TEntity> updated = Context.Set<TEntity>().Update(entityToUpdate);
        await Context.SaveChangesAsync();

        await UpsertComplementaryInformations(
            changeRecord.Id!.Value, toUpdate.ComplementaryInformations, updated.Entity.Id!.Value);
        await tracker.TrackUpdate(updated.Entity, oldValue, changeRecord.Id.Value);

        return updated.Entity;
    }

    public virtual async Task Delete(List<T> updated, ICollection<TEntity> existing, IChangeTracker tracker, Guid? changeRecordId = null)
    {
        var (toDelete, complementaryInformationsToDelete) =
            RepoUtils.FindMissingChangeableAndComplementaryInformationsForDeletion(
                [.. updated.Cast<Changeable>()],
                [.. existing.Cast<ChangeableEntity>()]);

        Logger.LogInformation(
            $"Found {toDelete.Count} {typeof(TEntity).Name} to delete with ids {string.Join(",", toDelete.Select(x => x.Id))}");

        Context.Set<TEntity>().RemoveRange(toDelete.Cast<TEntity>().ToList());
        Context.ComplementaryInformations.RemoveRange(complementaryInformationsToDelete);
        await Context.SaveChangesAsync();
    }

    protected async Task UpsertComplementaryInformations(
        Guid changeRecordId,
        ICollection<ComplementaryInformation> complementaryInformations,
        Guid changeableId)
    {
        foreach (ComplementaryInformation ci in complementaryInformations)
        {
            if (ci.Id == null)
            {
                Logger.LogInformation($"Adding complementary information to changeable with id {changeableId}");
                await _complementaryInformationRepository.Add(ci, changeRecordId, changeableId);
            }
            else
            {
                Logger.LogInformation($"Updating complementary information with id {ci.Id}");
                await _complementaryInformationRepository.Update(ci, changeRecordId);
            }
        }
    }
}