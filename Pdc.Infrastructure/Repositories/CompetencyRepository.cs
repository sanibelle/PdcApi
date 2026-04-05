using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.Common;
using Pdc.Domain.Models.CourseFramework;
using Pdc.Domain.Models.MinisterialSpecification;
using Pdc.Domain.Models.Versioning;
using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Entities.MinisterialSpecification;
using Pdc.Infrastructure.Entities.Version;

namespace Pdc.Infrastructure.Repositories;

public class CompetencyRepository(AppDbContext context, IComplementaryInformationRepository complementaryInformationRepository, IChangeRecordRepository changeRecordRepository, IMapper mapper, ILogger<CompetencyRepository> _logger) : ICompetencyRepository
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly IComplementaryInformationRepository _complementaryInformationRepository = complementaryInformationRepository;
    private readonly IChangeRecordRepository _changeRecordRepository = changeRecordRepository;

    public async Task<List<MinisterialCompetency>> GetAll()
    {
        List<CompetencyEntity> entities = await _context.Competencies.AsNoTracking().ToListAsync();
        return _mapper.Map<List<MinisterialCompetency>>(entities);
    }

    public async Task<MinisterialCompetency> Add(ProgramOfStudy program, MinisterialCompetency competency)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            _logger.LogInformation($"Adding competency with code {competency.Code} to program of study {program.Code}");
            ChangeRecord changeRecord = await _changeRecordRepository.AddChangeRecord(competency.ChangeRecord!);

            CompetencyEntity competencyEntity = _mapper.Map<CompetencyEntity>(competency);
            competencyEntity.ChangeRecordId = changeRecord.Id;
            competencyEntity.ProgramOfStudyCode = program.Code;

            _logger.LogInformation($"Mapped competency to entity with code {competencyEntity.Code} for program of study {program.Code}");
            EntityEntry<CompetencyEntity> addedCompetencyEntity = await _context.Competencies.AddAsync(competencyEntity);

            _logger.LogInformation($"Saved competency entity with code {addedCompetencyEntity.Entity.Code} to database for program of study {program.Code}");

            foreach (RealisationContext rc in competency.RealisationContexts)
            {
                await AddRealisationContexts(changeRecord, addedCompetencyEntity.Entity, rc);
            }
            foreach (MinisterialCompetencyElement ce in competency.CompetencyElements)
            {
                CompetencyElementEntity addedCompetencyElementEntity = await AddCompetencyElement(changeRecord, addedCompetencyEntity.Entity, ce);

                foreach (PerformanceCriteria pc in ce.PerformanceCriterias)
                {
                    await AddPerformanceCriterias(changeRecord, addedCompetencyElementEntity, pc);
                }
            }
            _logger.LogInformation($"Before commit transaction with code {addedCompetencyEntity.Entity.Code} to database for program of study {program.Code}");
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            _logger.LogInformation($"Transaction success with code {addedCompetencyEntity.Entity.Code} ");
            return _mapper.Map<MinisterialCompetency>(competencyEntity);
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    private async Task AddPerformanceCriterias(ChangeRecord changeRecord, CompetencyElementEntity competencyElementEntity, PerformanceCriteria pc)
    {
        _logger.LogInformation($"Adding performance criteria with position {pc.Position} to competency element with id {competencyElementEntity.Id}");
        PerformanceCriteriaEntity performanceCriteriaEntity = _mapper.Map<PerformanceCriteriaEntity>(pc);
        performanceCriteriaEntity.CompetencyElement = competencyElementEntity;
        EntityEntry<PerformanceCriteriaEntity> addedPerformanceCriteria = await _context.PerformanceCriterias.AddAsync(performanceCriteriaEntity);
        await _context.SaveChangesAsync();
        _logger.LogInformation($"Added performance criteria with position {pc.Position} to competency element with id {competencyElementEntity.Id}");
        await UpsertComplementaryInformations(changeRecord.Id!.Value, pc.ComplementaryInformations, addedPerformanceCriteria.Entity.Id!.Value);
    }

    private async Task<CompetencyElementEntity> AddCompetencyElement(ChangeRecord changeRecord, CompetencyEntity competencyEntity, MinisterialCompetencyElement ce)
    {
        _logger.LogInformation($"Adding competency element with position {ce.Position} to competency with code {competencyEntity.Code}");
        CompetencyElementEntity competencyElementEntity = _mapper.Map<CompetencyElementEntity>(ce);
        competencyElementEntity.Competency = competencyEntity;
        EntityEntry<CompetencyElementEntity> addedCompetencyElementEntity = await _context.CompetencyElements.AddAsync(competencyElementEntity);
        await _context.SaveChangesAsync();
        _logger.LogInformation($"Added competency element with position {ce.Position} to competency with code {competencyEntity.Code}");
        await UpsertComplementaryInformations(changeRecord.Id!.Value, ce.ComplementaryInformations, addedCompetencyElementEntity.Entity.Id!.Value);
        return addedCompetencyElementEntity.Entity;
    }

    private async Task AddRealisationContexts(ChangeRecord changeRecord, CompetencyEntity competencyEntity, RealisationContext rc)
    {
        _logger.LogInformation($"Adding realisation context to competency with code {competencyEntity.Code}");
        RealisationContextEntity realisationContextEntity = _mapper.Map<RealisationContextEntity>(rc);
        realisationContextEntity.Competency = competencyEntity;
        EntityEntry<RealisationContextEntity> addedRealisationContextEntity = await _context.RealisationContexts.AddAsync(realisationContextEntity);
        await _context.SaveChangesAsync();
        _logger.LogInformation($"Added realisation context to competency with code {competencyEntity.Code}");
        await UpsertComplementaryInformations(changeRecord.Id!.Value, rc.ComplementaryInformations, addedRealisationContextEntity.Entity.Id!.Value);
    }


    public async Task<MinisterialCompetency> Update(MinisterialCompetency competency)
    {

        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            _logger.LogInformation($"Updating competency with code {competency.Code}");
            CompetencyEntity existingCompetency = await FindCompetencyEntityByCode(competency.Code);

            await RemoveDeletedElements(competency, existingCompetency);

            _mapper.Map(competency, existingCompetency);
            await _context.SaveChangesAsync();

            foreach (RealisationContext rc in competency.RealisationContexts)
            {
                if (rc.Id == null)
                {
                    await AddRealisationContexts(competency.ChangeRecord!, existingCompetency, rc);
                }
                else
                {
                    await UpdateRealisationContexts(competency.ChangeRecord!, rc);
                }
            }
            foreach (MinisterialCompetencyElement ce in competency.CompetencyElements)
            {
                CompetencyElementEntity updatedCe = ce.Id == null
                    ? await AddCompetencyElement(competency.ChangeRecord!, existingCompetency, ce)
                    : await UpdateCompetencyElement(competency.ChangeRecord!, ce);
                foreach (PerformanceCriteria pc in ce.PerformanceCriterias)
                {
                    if (pc.Id == null)
                    {
                        await AddPerformanceCriterias(competency.ChangeRecord!, updatedCe, pc);
                    }
                    else
                    {
                        await UpdatePerformanceCriteria(competency.ChangeRecord!, pc);
                    }
                }
            }

            await _context.SaveChangesAsync();
            _logger.LogInformation($"Commiting competency with code {competency.Code}");
            await transaction.CommitAsync();
            _logger.LogInformation($"Comitted competency with code {competency.Code}");
            return await FindByCode(competency.Code);
        }
        catch
        {
            await transaction.RollbackAsync();
            _logger.LogError($"Failed to updated the competency with code {competency.Code}");
            throw;
        }
    }

    private async Task<CompetencyEntity> FindCompetencyEntityByCode(string code)
    {
        return await _context.Competencies
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

    private async Task<CompetencyElementEntity> UpdateCompetencyElement(ChangeRecord changeRecord, MinisterialCompetencyElement ce)
    {
        _logger.LogInformation($"Updating competency element with id {ce.Id}");
        CompetencyElementEntity ceToUpdate = await _context.CompetencyElements
            .Include(ce => ce.ComplementaryInformations)
            .SingleOrDefaultAsync(x => x.Id == ce.Id) ?? throw new NotFoundException(nameof(CompetencyElementEntity), ce.Id!);

        _mapper.Map(ce, ceToUpdate);
        EntityEntry<CompetencyElementEntity> updatedCompetencyElementEntity = _context.CompetencyElements.Update(ceToUpdate);
        await _context.SaveChangesAsync();
        _logger.LogInformation($"Updated competency element with id {ce.Id}");
        await UpsertComplementaryInformations(changeRecord.Id!.Value, ce.ComplementaryInformations, updatedCompetencyElementEntity.Entity.Id!.Value);
        return ceToUpdate;
    }

    private async Task UpdateRealisationContexts(ChangeRecord changeRecord, RealisationContext rc)
    {
        _logger.LogInformation($"Updating realisation context with id {rc.Id}");
        RealisationContextEntity rcToUpdate = await _context.RealisationContexts
            .Include(rc => rc.ComplementaryInformations)
            .SingleOrDefaultAsync(x => x.Id == rc.Id) ?? throw new NotFoundException(nameof(RealisationContextEntity), rc.Id!);

        _mapper.Map(rc, rcToUpdate);
        EntityEntry<RealisationContextEntity> updatedRealisationContextEntity = _context.RealisationContexts.Update(rcToUpdate);
        await _context.SaveChangesAsync();
        _logger.LogInformation($"Updated realisation context with id {rc.Id}");
        await UpsertComplementaryInformations(changeRecord.Id!.Value, rc.ComplementaryInformations, updatedRealisationContextEntity.Entity.Id!.Value);
    }

    private async Task UpdatePerformanceCriteria(ChangeRecord changeRecord, PerformanceCriteria pc)
    {
        _logger.LogInformation($"Updating performance criteria with id {pc.Id}");
        PerformanceCriteriaEntity pcToUpdate = await _context.PerformanceCriterias
            .Include(pc => pc.ComplementaryInformations)
            .SingleOrDefaultAsync(x => x.Id == pc.Id) ?? throw new NotFoundException(nameof(PerformanceCriteriaEntity), pc.Id!);

        _mapper.Map(pc, pcToUpdate);
        EntityEntry<PerformanceCriteriaEntity> updatedPerformanceCriteriaEntity = _context.PerformanceCriterias.Update(pcToUpdate);
        await _context.SaveChangesAsync();
        _logger.LogInformation($"Updated performance criteria with id {pc.Id}");
        await UpsertComplementaryInformations(changeRecord.Id!.Value, pc.ComplementaryInformations, updatedPerformanceCriteriaEntity.Entity.Id!.Value);
    }

    private async Task UpsertComplementaryInformations(Guid changeRecordId, ICollection<ComplementaryInformation> complementaryInformations, Guid changeableId)
    {
        foreach (ComplementaryInformation ci in complementaryInformations)
        {
            if (ci.Id == null)
            {
                _logger.LogInformation($"Adding complementary information to changeable with id {changeableId}");
                await _complementaryInformationRepository.Add(ci, changeRecordId, changeableId);
            }
            else
            {
                _logger.LogInformation($"Updating complementary information with id {ci.Id} for changeable with id {changeableId}");
                await _complementaryInformationRepository.Update(ci, changeRecordId);
            }
        }
    }


    private async Task RemoveDeletedElements(MinisterialCompetency competency, CompetencyEntity existingCompetency)
    {
        (List<ChangeableEntity> realisationContextToDelete, List<ComplementaryInformationEntity> realisationContextComplementaryInformationsToDelete) = RepoUtils.FindMissingAChangeableAndComplementaryInformationsForDeletion([.. competency.RealisationContexts.Cast<AChangeable>()], [.. existingCompetency.RealisationContexts.Cast<ChangeableEntity>()]);
        _logger.LogInformation($"Found {realisationContextToDelete.Count} realisation contexts to delete for competency with code {competency.Code} with ids ${string.Join(",", realisationContextToDelete.Select(x => x.Id))}");
        _context.RealisationContexts.RemoveRange(realisationContextToDelete.Cast<RealisationContextEntity>().ToList());

        (List<ChangeableEntity> competencyElementsToDelete, List<ComplementaryInformationEntity> competencyElementsComplementaryInformationsToDelete) = RepoUtils.FindMissingAChangeableAndComplementaryInformationsForDeletion([.. competency.CompetencyElements.Cast<AChangeable>()], [.. existingCompetency.CompetencyElements.Cast<ChangeableEntity>()]);
        _logger.LogInformation($"Found {competencyElementsToDelete.Count} competency elements to delete for competency with code {competency.Code} with ids ${string.Join(",", competencyElementsToDelete.Select(x => x.Id))}");
        _context.CompetencyElements.RemoveRange(competencyElementsToDelete.Cast<CompetencyElementEntity>().ToList());


        (List<ChangeableEntity> performanceCriteriasToDelete, List<ComplementaryInformationEntity> performanceCriteriasComplementaryInformationsToDelete) = RepoUtils.FindMissingAChangeableAndComplementaryInformationsForDeletion([.. competency.CompetencyElements.SelectMany(x => x.PerformanceCriterias).Cast<AChangeable>()], [.. existingCompetency.CompetencyElements.SelectMany(x => x.PerformanceCriterias).Cast<ChangeableEntity>()]);
        _logger.LogInformation($"Found {performanceCriteriasToDelete.Count} performance criteria to delete for competency with code {competency.Code} with ids ${string.Join(",", performanceCriteriasToDelete.Select(x => x.Id))}");
        _context.PerformanceCriterias.RemoveRange(performanceCriteriasToDelete.Cast<PerformanceCriteriaEntity>().ToList());

        List<ComplementaryInformationEntity> allComplementaryInformationsToDelete = realisationContextComplementaryInformationsToDelete
            .Concat(competencyElementsComplementaryInformationsToDelete)
            .Concat(performanceCriteriasComplementaryInformationsToDelete)
            .ToList();
        _logger.LogInformation($"Found {allComplementaryInformationsToDelete.Count} complementary informations to delete for competency with code {competency.Code} with ids ${string.Join(",", allComplementaryInformationsToDelete.Select(x => x.Id))}");
        _context.ComplementaryInformations.RemoveRange(allComplementaryInformationsToDelete);

        await _context.SaveChangesAsync();
    }

    public async Task Delete(string programOfStudyCode, string competencyCode)
    {
        CompetencyEntity? entity = await _context.Competencies
            .Include(x => x.ProgramOfStudy)
            .SingleOrDefaultAsync(x => x.Code == competencyCode && x.ProgramOfStudy.Code == programOfStudyCode)
            ?? throw new NotFoundException(nameof(CompetencyEntity), competencyCode);

        _context.Competencies.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<MinisterialCompetency> FindByCode(string competencyCode)
    {
        CompetencyEntity entity = await FindDetailedEntityByCode(competencyCode);
        return _mapper.Map<MinisterialCompetency>(entity);
    }

    public async Task<bool> ExistsEntityByCode(string programOfStudyCode, string competencyCode)
    {
        return await _context.Competencies
            .Include(x => x.ProgramOfStudy)
            .Where(x => x.Code == competencyCode && x.ProgramOfStudyCode == programOfStudyCode)
            .AnyAsync();
    }

    private async Task<CompetencyEntity> FindDetailedEntityByCode(string competencyCode)
    {
        CompetencyEntity? competency = await _context.Competencies
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
                .ThenInclude(v => v!.CreatedBy)
            .SingleOrDefaultAsync(x => x.Code == competencyCode);
        return competency ?? throw new NotFoundException(nameof(MinisterialCompetency), competencyCode);
    }

    public async Task<List<MinisterialCompetency>> GetByProgramOfStudy(string programOfStudyCode)
    {
        List<CompetencyEntity> comps = await _context.Competencies
            .AsNoTracking()
            .Include(c => c.ChangeRecord)
                .ThenInclude(v => v!.CreatedBy)
            .Where(c => c.ProgramOfStudy != null && c.ProgramOfStudy.Code == programOfStudyCode)
            .ToListAsync();
        return _mapper.Map<List<MinisterialCompetency>>(comps);
    }
}
