using AutoMapper;
using AutoMapper.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.CourseFramework;
using Pdc.Domain.Models.MinisterialSpecification;
using Pdc.Domain.Models.Versioning;
using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Entities.CourseFramework;
using Pdc.Infrastructure.Entities.MinisterialSpecification;
using Pdc.Infrastructure.Entities.Versioning;
using Pdc.Infrastructure.Exceptions;

namespace Pdc.Infrastructure.Repositories;

public class CompetencyRepository : ICompetencyRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CompetencyRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<MinisterialCompetency>> GetAll()
    {
        List<CompetencyEntity> entities = await _context.Competencies.AsNoTracking().ToListAsync();
        return _mapper.Map<List<MinisterialCompetency>>(entities);
    }

    public async Task<MinisterialCompetency> Add(ProgramOfStudy program, MinisterialCompetency competency)
    {
        ProgramOfStudyEntity programEntity = await FindProgramOfStudy(program.Code);
        CompetencyEntity entity = await _context.Competencies
            .Persist(_mapper)
            .InsertOrUpdateAsync(competency);
        entity.ProgramOfStudy = programEntity;
        await _context.SaveChangesAsync();
        return _mapper.Map<MinisterialCompetency>(entity);
    }

    public async Task<MinisterialCompetency> Update(MinisterialCompetency competency)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            await RemoveDeletedElements(competency);
            var test = _context.ChangeTracker.DebugView.LongView;
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            CompetencyEntity entity = await _context.Competencies
                .Persist(_mapper)
                .InsertOrUpdateAsync(competency);
            test = _context.ChangeTracker.DebugView.LongView;
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();
            return _mapper.Map<MinisterialCompetency>(entity);
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    private async Task RemoveDeletedElements(MinisterialCompetency competency)
    {
        CompetencyEntity? existingCompetency = await _context.Competencies
            .AsNoTracking()
            .AsSplitQuery()
            .Include(c => c.RealisationContexts)
                .ThenInclude(rc => rc.ComplementaryInformations)
            .Include(c => c.CompetencyElements)
                .ThenInclude(ce => ce.ComplementaryInformations)
            .Include(c => c.CompetencyElements)
                .ThenInclude(ce => ce.PerformanceCriterias)
                    .ThenInclude(pc => pc.ComplementaryInformations)
            .Include(c => c.ProgramOfStudy)
            .SingleOrDefaultAsync(x => x.Code == competency.Code);
        if (existingCompetency == null)
        {
            throw new EntityNotFoundException(nameof(MinisterialCompetency), competency.Code);
        }
        (List<ChangeableEntity> realisationContextToDelete, List<ComplementaryInformationEntity> realisationContextComplementaryInformationsToDelete) = RepoUtils.FindMissingAChangeableAndComplementaryInformationsForDeletion(competency.RealisationContexts.Cast<AChangeable>().ToList(), existingCompetency.RealisationContexts.Cast<ChangeableEntity>().ToList());
        (List<ChangeableEntity> competencyElementsToDelete, List<ComplementaryInformationEntity> competencyElementsComplementaryInformationsToDelete) = RepoUtils.FindMissingAChangeableAndComplementaryInformationsForDeletion(competency.CompetencyElements.Cast<AChangeable>().ToList(), existingCompetency.CompetencyElements.Cast<ChangeableEntity>().ToList());
        (List<ChangeableEntity> performanceCriteriasToDelete, List<ComplementaryInformationEntity> performanceCriteriasComplementaryInformationsToDelete) = RepoUtils.FindMissingAChangeableAndComplementaryInformationsForDeletion(competency.CompetencyElements.SelectMany(x => x.PerformanceCriterias).Cast<AChangeable>().ToList(), existingCompetency.CompetencyElements.SelectMany(x => x.PerformanceCriterias).Cast<ChangeableEntity>().ToList());

        _context.ComplementaryInformations.AttachRange(realisationContextComplementaryInformationsToDelete);
        _context.ComplementaryInformations.RemoveRange(realisationContextComplementaryInformationsToDelete);

        _context.ComplementaryInformations.AttachRange(competencyElementsComplementaryInformationsToDelete);
        _context.ComplementaryInformations.RemoveRange(competencyElementsComplementaryInformationsToDelete);

        _context.ComplementaryInformations.AttachRange(performanceCriteriasComplementaryInformationsToDelete);
        _context.ComplementaryInformations.RemoveRange(performanceCriteriasComplementaryInformationsToDelete);

        _context.RealisationContexts.AttachRange(realisationContextToDelete.Cast<RealisationContextEntity>().ToList());
        _context.RealisationContexts.RemoveRange(realisationContextToDelete.Cast<RealisationContextEntity>().ToList());

        _context.PerformanceCriterias.AttachRange(performanceCriteriasToDelete.Cast<PerformanceCriteriaEntity>().ToList());
        _context.PerformanceCriterias.RemoveRange(performanceCriteriasToDelete.Cast<PerformanceCriteriaEntity>().ToList());

        _context.CompetencyElements.AttachRange(competencyElementsToDelete.Cast<CompetencyElementEntity>().ToList());
        _context.CompetencyElements.RemoveRange(competencyElementsToDelete.Cast<CompetencyElementEntity>().ToList());
    }

    public async Task Delete(string programOfStudyCode, string competencyCode)
    {
        CompetencyEntity? entity = await _context.Competencies.SingleOrDefaultAsync(x => x.Code == competencyCode && x.ProgramOfStudy.Code == programOfStudyCode);
        if (entity == null)
        {
            throw new EntityNotFoundException(nameof(MinisterialCompetency), competencyCode);
        }
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
            .Where(x => x.Code == competencyCode && x.ProgramOfStudy.Code == programOfStudyCode)
            .AnyAsync();
    }

    private async Task<CompetencyEntity> FindDetailedEntityByCode(string competencyCode)
    {
        CompetencyEntity? competency = await _context.Competencies
            .AsSplitQuery()
            .AsNoTracking()
            .Include(c => c.RealisationContexts)
                .ThenInclude(rc => rc.ComplementaryInformations)
                    .ThenInclude(cr => cr.WrittenOnVersion)
                        .ThenInclude(ci => ci!.CreatedBy)
            .Include(c => c.CompetencyElements)
                .ThenInclude(ce => ce.ComplementaryInformations)
                    .ThenInclude(cr => cr.WrittenOnVersion)
                        .ThenInclude(ci => ci!.CreatedBy)
            .Include(c => c.CompetencyElements)
                .ThenInclude(ce => ce.PerformanceCriterias)
                    .ThenInclude(pc => pc.ComplementaryInformations)
                        .ThenInclude(cr => cr.WrittenOnVersion)
                            .ThenInclude(ci => ci!.CreatedBy)
            .Include(c => c.ProgramOfStudy)
            .Include(c => c.CurrentVersion)
                .ThenInclude(v => v.CreatedBy)
            .SingleOrDefaultAsync(x => x.Code == competencyCode);

        if (competency == null)
        {
            throw new EntityNotFoundException(nameof(MinisterialCompetency), competencyCode);
        }

        return competency;
    }

    private async Task<ProgramOfStudyEntity> FindProgramOfStudy(string code)
    {
        ProgramOfStudyEntity? program = await _context.ProgramOfStudies
            .SingleOrDefaultAsync(x => x.Code == code);
        if (program == null)

        {
            throw new EntityNotFoundException(nameof(ProgramOfStudy), code);
        }
        return program;
    }

    public async Task<List<MinisterialCompetency>> GetByProgramOfStudy(string programOfStudyCode)
    {
        List<CompetencyEntity> comps = await _context.Competencies
            .AsNoTracking()
            .Include(c => c.CurrentVersion)
                .ThenInclude(v => v.CreatedBy)
            .Where(c => c.ProgramOfStudy.Code == programOfStudyCode)
            .ToListAsync();
        return _mapper.Map<List<MinisterialCompetency>>(comps);
    }
}
