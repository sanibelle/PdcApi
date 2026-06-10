using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.Common;
using Pdc.Domain.Models.CourseFramework;
using Pdc.Domain.Models.MinisterialSpecification;
using Pdc.Domain.Models.Versioning;
using Pdc.Infrastructure.ChangeTracker;
using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Entities.MinisterialSpecification;
using Pdc.Infrastructure.Interfaces;

namespace Pdc.Infrastructure.Repositories;

internal class CompetencyRepository(
    AppDbContext context,
    IChangeRecordRepository changeRecordRepository,
    IMapper mapper,
    ILogger<CompetencyRepository> logger,
    [FromKeyedServices("tracked")] IChangeApplier<RealisationContext, CompetencyEntity, RealisationContextEntity> trackedRealisationContextChangeApplier,
    [FromKeyedServices("tracked")] IChangeApplier<MinisterialCompetencyElement, CompetencyEntity, CompetencyElementEntity> trackedCompetencyElementChangeApplier,
    [FromKeyedServices("tracked")] IChangeApplier<PerformanceCriteria, CompetencyElementEntity, PerformanceCriteriaEntity> trackedPerformanceCriteriaChangeApplier,
    [FromKeyedServices("tracked")] IChangeApplier<RealisationContext, CompetencyEntity, RealisationContextEntity> untrackedRealisationContextChangeApplier,
    [FromKeyedServices("untracked")] IChangeApplier<MinisterialCompetencyElement, CompetencyEntity, CompetencyElementEntity> untrackedCompetencyElementChangeApplier,
    [FromKeyedServices("untracked")] IChangeApplier<PerformanceCriteria, CompetencyElementEntity, PerformanceCriteriaEntity> untrackedPerformanceCriteriaChangeApplier) : ICompetencyRepository
{
    public async Task<List<MinisterialCompetency>> GetAll()
    {
        List<CompetencyEntity> entities = await context.Competencies.AsNoTracking().ToListAsync();
        return mapper.Map<List<MinisterialCompetency>>(entities);
    }

    public async Task<MinisterialCompetency> Add(ProgramOfStudy program, MinisterialCompetency competency)
    {
        using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            IChangeTracker tracker = new NoOpChangeDetailsTracker();
            logger.LogInformation($"Adding competency with code {competency.Code} to program of study {program.Code}");
            ChangeRecord changeRecord = await changeRecordRepository.AddChangeRecord(competency.ChangeRecord!);

            CompetencyEntity competencyEntity = mapper.Map<CompetencyEntity>(competency);
            competencyEntity.ChangeRecordId = changeRecord.Id;
            competencyEntity.ProgramOfStudyCode = program.Code;

            logger.LogInformation($"Mapped competency to entity with code {competencyEntity.Code} for program of study {program.Code}");
            EntityEntry<CompetencyEntity> addedCompetencyEntity = await context.Competencies.AddAsync(competencyEntity);

            logger.LogInformation($"Saved competency entity with code {addedCompetencyEntity.Entity.Code} to database for program of study {program.Code}");

            foreach (RealisationContext rc in competency.RealisationContexts)
            {
                await untrackedRealisationContextChangeApplier.Add(changeRecord, addedCompetencyEntity.Entity, rc, tracker);
            }
            foreach (MinisterialCompetencyElement ce in competency.CompetencyElements)
            {
                CompetencyElementEntity addedCompetencyElementEntity = await untrackedCompetencyElementChangeApplier.Add(changeRecord, addedCompetencyEntity.Entity, ce, tracker);

                foreach (PerformanceCriteria pc in ce.PerformanceCriterias)
                {
                    await untrackedPerformanceCriteriaChangeApplier.Add(changeRecord, addedCompetencyElementEntity, pc, tracker);
                }
            }

            logger.LogInformation($"Before commit transaction with code {addedCompetencyEntity.Entity.Code} to database for program of study {program.Code}");
            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            logger.LogInformation($"Transaction success with code {addedCompetencyEntity.Entity.Code} ");
            return mapper.Map<MinisterialCompetency>(competencyEntity);
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<MinisterialCompetency> UpdateTrackedChangeable(MinisterialCompetency competency)
    {
        using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            IChangeTracker tracker = new ChangeDetailsTracker(context);
            logger.LogInformation($"Updating competency with code {competency.Code}");
            CompetencyEntity existingCompetency = await FindCompetencyEntityByCode(competency.Code);

            // if the id is null, then we must add the change record.
            if (competency.ChangeRecord.Id == null)
            {
                ChangeRecord changeRecord = await changeRecordRepository.AddChangeRecord(competency.ChangeRecord!);
                competency.ChangeRecord = changeRecord;
            }

            await UpdateCompetencyAndChilds(competency, tracker, existingCompetency, trackedRealisationContextChangeApplier, trackedCompetencyElementChangeApplier, trackedPerformanceCriteriaChangeApplier);

            logger.LogInformation($"Commiting competency with code {competency.Code}");
            await transaction.CommitAsync();
            logger.LogInformation($"Comitted competency with code {competency.Code}");
            return await FindByCode(competency.Code);
        }
        catch
        {
            await transaction.RollbackAsync();
            logger.LogError($"Failed to updated the competency with code {competency.Code}");
            throw;
        }
    }


    public async Task<MinisterialCompetency> UpdateUntrackedChangeable(MinisterialCompetency competency)
    {
        using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            IChangeTracker tracker = new NoOpChangeDetailsTracker();
            logger.LogInformation($"Updating competency with code {competency.Code}");
            CompetencyEntity existingCompetency = await FindCompetencyEntityByCode(competency.Code);

            await UpdateCompetencyAndChilds(competency, tracker, existingCompetency, untrackedRealisationContextChangeApplier, untrackedCompetencyElementChangeApplier, untrackedPerformanceCriteriaChangeApplier);
            logger.LogInformation($"Commiting competency with code {competency.Code}");
            await transaction.CommitAsync();
            logger.LogInformation($"Comitted competency with code {competency.Code}");
            return await FindByCode(competency.Code);
        }
        catch
        {
            await transaction.RollbackAsync();
            logger.LogError($"Failed to updated the competency with code {competency.Code}");
            throw;
        }
    }

    private async Task<CompetencyEntity> FindCompetencyEntityByCode(string code)
    {
        return await context.Competencies
                        .AsSplitQuery()
                        .Include(c => c.RealisationContexts)
                            .ThenInclude(rc => rc.ComplementaryInformations)
                        .Include(c => c.CompetencyElements)
                            .ThenInclude(ce => ce.ComplementaryInformations)
                        .Include(c => c.CompetencyElements)
                            .ThenInclude(ce => ce.PerformanceCriterias)
                                .ThenInclude(pc => pc.ComplementaryInformations)
                        .Include(c => c.ProgramOfStudy)
                        .Include(c => c.ChangeRecord)
                        .SingleOrDefaultAsync(x => x.Code == code) ?? throw new NotFoundException(nameof(MinisterialCompetency), code);
    }

    private async Task UpdateCompetencyAndChilds(MinisterialCompetency competency,
                                                 IChangeTracker tracker,
                                                 CompetencyEntity existingCompetency,
                                                 IChangeApplier<RealisationContext, CompetencyEntity, RealisationContextEntity> realisationContextChangeApplier,
                                                 IChangeApplier<MinisterialCompetencyElement, CompetencyEntity, CompetencyElementEntity> competencyElementChangeApplier,
                                                 IChangeApplier<PerformanceCriteria, CompetencyElementEntity, PerformanceCriteriaEntity> performanceCriteriaChangeApplier)
    {
        mapper.Map(competency, existingCompetency);
        await context.SaveChangesAsync();

        await realisationContextChangeApplier.Delete(competency.RealisationContexts, existingCompetency.RealisationContexts, tracker, competency.ChangeRecord.Id);
        await competencyElementChangeApplier.Delete(competency.CompetencyElements, existingCompetency.CompetencyElements, tracker, competency.ChangeRecord.Id);
        await performanceCriteriaChangeApplier.Delete(competency.CompetencyElements.SelectMany(x => x.PerformanceCriterias).ToList(), existingCompetency.CompetencyElements.SelectMany(x => x.PerformanceCriterias).ToList(), tracker, competency.ChangeRecord.Id);

        foreach (RealisationContext rc in competency.RealisationContexts)
        {
            if (rc.Id == null)
            {
                await realisationContextChangeApplier.Add(competency.ChangeRecord!, existingCompetency, rc, tracker);
            }
            else
            {
                await realisationContextChangeApplier.Update(competency.ChangeRecord!, rc, tracker);
            }
        }
        foreach (MinisterialCompetencyElement ce in competency.CompetencyElements)
        {
            CompetencyElementEntity updatedCe = ce.Id == null
                ? await competencyElementChangeApplier.Add(competency.ChangeRecord!, existingCompetency, ce, tracker)
                : await competencyElementChangeApplier.Update(competency.ChangeRecord!, ce, tracker);
            foreach (PerformanceCriteria pc in ce.PerformanceCriterias)
            {
                if (pc.Id == null)
                {
                    await performanceCriteriaChangeApplier.Add(competency.ChangeRecord!, updatedCe, pc, tracker);
                }
                else
                {
                    await performanceCriteriaChangeApplier.Update(competency.ChangeRecord!, pc, tracker);
                }
            }
        }

        await context.SaveChangesAsync();
    }

    public async Task Delete(string programOfStudyCode, string competencyCode)
    {
        CompetencyEntity? entity = await context.Competencies
            .Include(x => x.ProgramOfStudy)
            .SingleOrDefaultAsync(x => x.Code == competencyCode && x.ProgramOfStudy.Code == programOfStudyCode)
            ?? throw new NotFoundException(nameof(CompetencyEntity), competencyCode);

        context.Competencies.Remove(entity);
        await context.SaveChangesAsync();
    }

    // XXX Question client: Est-ce qu'une compétence peut avoir le même code dans un autre programme?
    public async Task<MinisterialCompetency> FindByCode(string competencyCode)
    {
        CompetencyEntity entity = await FindDetailedEntityByCode(competencyCode);
        return mapper.Map<MinisterialCompetency>(entity);
    }

    public async Task<bool> ExistsEntityByCode(string programOfStudyCode, string competencyCode)
    {
        return await context.Competencies
            .Include(x => x.ProgramOfStudy)
            .Where(x => x.Code == competencyCode && x.ProgramOfStudyCode == programOfStudyCode)
            .AnyAsync();
    }

    private async Task<CompetencyEntity> FindDetailedEntityByCode(string competencyCode)
    {
        CompetencyEntity? competency = await context.Competencies
            .AsSplitQuery()
            .AsNoTrackingWithIdentityResolution()
            .Include(c => c.RealisationContexts)
                .ThenInclude(rc => rc.ComplementaryInformations!)
                    .ThenInclude(cr => cr.ChangeRecord)
                        .ThenInclude(ci => ci!.CreatedBy)
            .Include(c => c.CompetencyElements)
                .ThenInclude(ce => ce.ComplementaryInformations!)
                    .ThenInclude(cr => cr.ChangeRecord)
                        .ThenInclude(ci => ci!.CreatedBy)
            .Include(c => c.CompetencyElements)
                .ThenInclude(ce => ce.PerformanceCriterias)
                    .ThenInclude(pc => pc.ComplementaryInformations!)
                        .ThenInclude(cr => cr.ChangeRecord)
                            .ThenInclude(ci => ci!.CreatedBy)
            .Include(c => c.ProgramOfStudy)
            .Include(c => c.ChangeRecord)
                .ThenInclude(v => v.CreatedBy)
            .Include(c => c.ChangeRecord)
                .ThenInclude(v => v.NextChangeRecord)
            .Include(c => c.ChangeRecord)
                .ThenInclude(v => v!.ParentChangeRecord)
            .SingleOrDefaultAsync(x => x.Code == competencyCode);
        return competency ?? throw new NotFoundException(nameof(MinisterialCompetency), competencyCode);
    }

    public async Task<List<MinisterialCompetency>> GetByProgramOfStudy(string programOfStudyCode)
    {
        List<CompetencyEntity> comps = await context.Competencies
            .AsNoTracking()
            .Include(c => c.ChangeRecord)
                .ThenInclude(v => v!.CreatedBy)
            .Where(c => c.ProgramOfStudy != null && c.ProgramOfStudy.Code == programOfStudyCode)
            .ToListAsync();
        return mapper.Map<List<MinisterialCompetency>>(comps);
    }
}
