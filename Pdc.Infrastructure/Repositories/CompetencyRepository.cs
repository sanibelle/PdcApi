using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.CourseFramework;
using Pdc.Domain.Models.MinisterialSpecification;
using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Entities.CourseFramework;
using Pdc.Infrastructure.Entities.MinisterialSpecification;
using Pdc.Infrastructure.Exceptions;

namespace Pdc.Infrastructure.Repositories;

public class CompetencyRepository : ICompetencyRespository
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

    public async Task<MinisterialCompetency> Add(ProgramOfStudy program, MinisterialCompetency competency)
    {
        var competencyEntity = _mapper.Map<CompetencyEntity>(competency);

        EntityEntry<CompetencyEntity> addedEntity = await _context.Competencies.AddAsync(competencyEntity);
        ProgramOfStudyEntity programEntity = await FindProgramOfStudy(program.Code);
        programEntity.Competencies.Add(addedEntity.Entity);
        await _context.SaveChangesAsync();
        return _mapper.Map<MinisterialCompetency>(addedEntity.Entity);
    }

    public async Task<MinisterialCompetency> Update(MinisterialCompetency competency)
    {
        CompetencyEntity entity = await FindEntityByCode(competency.ProgramOfStudyCode, competency.Code);
        _mapper.Map(competency, entity);
        EntityEntry<CompetencyEntity> updatedEntity = _context.Competencies.Update(entity);
        await _context.SaveChangesAsync();
        return _mapper.Map<MinisterialCompetency>(updatedEntity.Entity);
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
            .Include(c => c.RealisationContexts)
            .Include(c => c.CompetencyElements)
            .Where(x => x.Code == competencyCode && x.ProgramOfStudy.Code == programOfStudyCode)
            .AnyAsync();
    }

    private async Task<CompetencyEntity> FindEntityByCode(string programOfStudyCode, string competencyCode)
    {
        CompetencyEntity? competency = await _context.Competencies
            .Include(c => c.RealisationContexts)
            .Include(c => c.CompetencyElements)
            .Include(c => c.ProgramOfStudy)
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
}
