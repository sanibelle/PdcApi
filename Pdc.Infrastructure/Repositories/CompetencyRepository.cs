using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Pdc.Domain.Entities.MinisterialSpecification;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Entities.MinisterialSpecification;

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

    public async Task<MinisterialCompetency> Add(MinisterialCompetency competency)
    {
        EntityEntry<CompetencyEntity> entity = await _context.Competencies.AddAsync(_mapper.Map<CompetencyEntity>(competency));
        await _context.SaveChangesAsync();
        return _mapper.Map<MinisterialCompetency>(entity.Entity);
    }

    public async Task<MinisterialCompetency> Update(MinisterialCompetency competency)
    {
        CompetencyEntity entity = await FindEntityByCode(competency.Code);
        _mapper.Map(competency, entity);
        EntityEntry<CompetencyEntity> updatedEntity = _context.Competencies.Update(entity);
        await _context.SaveChangesAsync();
        return _mapper.Map<MinisterialCompetency>(updatedEntity.Entity);
    }

    public async Task Delete(string code)
    {
        CompetencyEntity entity = await FindEntityByCode(code);
        _context.Competencies.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<MinisterialCompetency> FindByCode(string code)
    {
        CompetencyEntity entity = await FindEntityByCode(code);
        return _mapper.Map<MinisterialCompetency>(entity);
    }

    private async Task<CompetencyEntity> FindEntityByCode(string code)
    {
        CompetencyEntity? competency = await _context.Competencies
            .Include(c => c.RealisationContexts)
            .Include(c => c.CompetencyElements)
            .SingleOrDefaultAsync(x => x.Code == code);
        if (competency == null)
        {
            throw new EntityNotFoundException(nameof(MinisterialCompetency), code);
        }
        return competency;
    }
}
