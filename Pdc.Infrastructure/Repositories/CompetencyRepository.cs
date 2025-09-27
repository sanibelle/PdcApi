using AutoMapper;
using AutoMapper.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.CourseFramework;
using Pdc.Domain.Models.MinisterialSpecification;
using Pdc.Domain.Models.Security;
using Pdc.Domain.Models.Versioning;
using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Entities.CourseFramework;
using Pdc.Infrastructure.Entities.Identity;
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

    public async Task<MinisterialCompetency> Add(ProgramOfStudy program, MinisterialCompetency competency, User currentUser)
    {
        CompetencyEntity entity = await _context.Competencies
            .Persist(_mapper)
            .InsertOrUpdateAsync(competency);
        // TODO move that into the usecase logic.
        IdentityUserEntity? user = await _context.Users.FirstOrDefaultAsync(x => x.Id == currentUser.Id);
        if (user is null)
        {
            throw new EntityNotFoundException(nameof(IdentityUserEntity), currentUser?.Id != null ? currentUser.Id : "id not found");
        }
        entity.SetCreatedBy(user);
        // End of TODO.
        var addedEntity = _context.Competencies.Add(entity);
        ProgramOfStudyEntity programEntity = await FindProgramOfStudy(program.Code);
        programEntity.Competencies.Add(addedEntity.Entity);
        await _context.SaveChangesAsync();
        return _mapper.Map<MinisterialCompetency>(entity);
    }

    public async Task<MinisterialCompetency> Update(MinisterialCompetency competency)
    {
        CompetencyEntity existingCompetency = await FindEntityByCode(competency.Code);
        MarkAllRemovedElementsForDeletion(competency, existingCompetency);
        CompetencyEntity entity = await _context.Competencies
            .Persist(_mapper)
            .InsertOrUpdateAsync(competency);

        await _context.SaveChangesAsync();
        return _mapper.Map<MinisterialCompetency>(entity);
    }

    private void MarkAllRemovedElementsForDeletion(MinisterialCompetency competency, CompetencyEntity existingCompetency)
    {
        List<ChangeableEntity> realisationContextToDelete = FindMissingAChangeableForDeletion(competency.RealisationContexts.Cast<AChangeable>().ToList(), existingCompetency.RealisationContexts.Cast<ChangeableEntity>().ToList());
        _context.RealisationContexts.RemoveRange(realisationContextToDelete.Cast<RealisationContextEntity>().ToList());
    }

    //TODO put that into an utils?
    private List<ChangeableEntity> FindMissingAChangeableForDeletion(List<AChangeable> listWithMissing, List<ChangeableEntity> listToCompare)
    {
        return listToCompare
            .Where(x => !listWithMissing
            .Select(y => y.Id).Contains(x.Id))
            .ToList();
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
        CompetencyEntity entity = await FindEntityByCode(competencyCode);
        return _mapper.Map<MinisterialCompetency>(entity);
    }

    public async Task<bool> ExistsEntityByCode(string programOfStudyCode, string competencyCode)
    {
        return await _context.Competencies
            .Where(x => x.Code == competencyCode && x.ProgramOfStudy.Code == programOfStudyCode)
            .AnyAsync();
    }

    private async Task<CompetencyEntity> FindEntityByCode(string competencyCode)
    {
        CompetencyEntity? competency = await _context.Competencies.AsNoTracking()
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
            .Include(p => p.Competencies)
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
