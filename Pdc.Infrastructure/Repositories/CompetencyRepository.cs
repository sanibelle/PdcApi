using AutoMapper;
using AutoMapper.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.CourseFramework;
using Pdc.Domain.Models.MinisterialSpecification;
using Pdc.Domain.Models.Security;
using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Entities.CourseFramework;
using Pdc.Infrastructure.Entities.Identity;
using Pdc.Infrastructure.Entities.MinisterialSpecification;
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
        List<CompetencyEntity> entities = await _context.Competencies.ToListAsync();
        return _mapper.Map<List<MinisterialCompetency>>(entities);
    }

    public async Task<MinisterialCompetency> Add(ProgramOfStudy program, MinisterialCompetency competency, User currentUser)
    {
        var competencyEntity = _mapper.Map<CompetencyEntity>(competency);
        // TODO move that into the usecase logic.
        IdentityUserEntity? user = _context.Users.FirstOrDefault(x => x.Id == currentUser.Id);
        if (user is null)
        {
            throw new EntityNotFoundException(nameof(IdentityUserEntity), currentUser.Id);
        }
        competencyEntity.SetCreatedBy(user);
        // End of TODO.
        Console.WriteLine(_context.ChangeTracker.DebugView.LongView);
        var addedEntity = _context.Competencies.Add(competencyEntity);
        ProgramOfStudyEntity programEntity = await FindProgramOfStudy(program.Code);
        programEntity.Competencies.Add(addedEntity.Entity);
        await _context.SaveChangesAsync();
        return _mapper.Map<MinisterialCompetency>(competencyEntity);
    }

    public async Task<MinisterialCompetency> Update(MinisterialCompetency competency)
    {
        //CompetencyEntity entity = await FindEntityByCode(competency.ProgramOfStudyCode, competency.Code);
        //_mapper.Map(competency, entity);
        //Console.WriteLine(_context.ChangeTracker.DebugView.ShortView);
        //_context.ChangeTracker.Clear();
        CompetencyEntity entity2 = await _context.Competencies
            .Persist(_mapper)
            .InsertOrUpdateAsync(competency);
        Console.WriteLine(_context.ChangeTracker.DebugView.ShortView);
        await _context.SaveChangesAsync();
        return _mapper.Map<MinisterialCompetency>(entity2);
    }

    public async Task Delete(string programOfStudyCode, string competencyCode)
    {
        CompetencyEntity entity = await FindEntityByCode(programOfStudyCode, competencyCode);
        _context.Competencies.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<MinisterialCompetency> FindByCode(string programOfStudyCode, string competencyCode)
    {
        CompetencyEntity entity = await FindEntityByCode(programOfStudyCode, competencyCode);
        return _mapper.Map<MinisterialCompetency>(entity);
    }

    public async Task<bool> ExistsEntityByCode(string programOfStudyCode, string competencyCode)
    {
        return await _context.Competencies
            .Where(x => x.Code == competencyCode && x.ProgramOfStudy.Code == programOfStudyCode)
            .AnyAsync();
    }

    private async Task<CompetencyEntity> FindEntityByCode(string programOfStudyCode, string competencyCode)
    {
        CompetencyEntity? competency = await _context.Competencies
            .Include(c => c.RealisationContexts)
                .ThenInclude(rc => rc.ComplementaryInformations)
                    .ThenInclude(cr => cr.WrittenOnVersion)
                        .ThenInclude(ci => ci.CreatedBy)
            .Include(c => c.CompetencyElements)
                .ThenInclude(ce => ce.ComplementaryInformations)
                    .ThenInclude(cr => cr.WrittenOnVersion)
                        .ThenInclude(ci => ci.CreatedBy)
            .Include(c => c.CompetencyElements)
                .ThenInclude(ce => ce.PerformanceCriterias)
                    .ThenInclude(pc => pc.ComplementaryInformations)
                        .ThenInclude(cr => cr.WrittenOnVersion)
                            .ThenInclude(ci => ci.CreatedBy)
            .Include(c => c.ProgramOfStudy)
            .Include(c => c.CurrentVersion)
                .ThenInclude(v => v.CreatedBy)
            .SingleOrDefaultAsync(x => x.Code == competencyCode && x.ProgramOfStudy.Code == programOfStudyCode);

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
        ProgramOfStudyEntity programEntity = await FindProgramOfStudy(programOfStudyCode);
        return _mapper.Map<List<MinisterialCompetency>>(programEntity.Competencies.ToList());
    }
}
