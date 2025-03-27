using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Pdc.Domain.Entities.MinisterialSpecification;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Entities.MinisterialSpecification;

namespace Pdc.Infrastructure.Repositories;

public class MinisterialCompetencyRepository : ICompetencyRespository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public MinisterialCompetencyRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<Competency>> GetAll()
    {
        List<CompetencyEntity> entities = await _context.Competencies.ToListAsync();
        return _mapper.Map<List<Competency>>(entities);
    }

    public async Task<Competency> Add(Competency competency)
    {
        EntityEntry<CompetencyEntity> entity = await _context.Competencies.AddAsync(_mapper.Map<CompetencyEntity>(competency));
        await _context.SaveChangesAsync();
        return _mapper.Map<Competency>(entity.Entity);
    }

    public async Task<Competency> Update(Competency competency)
    {

        CompetencyEntity entity = await FindEntityByCode(competency.Code);
        _mapper.Map(competency, entity);
        EntityEntry<CompetencyEntity> updatedCompetencyEntityEntry = _context.Competencies.Update(entity);
        await _context.SaveChangesAsync();
        return _mapper.Map<Competency>(updatedCompetencyEntityEntry.Entity);
    }

    public async Task Delete(string code)
    {
        CompetencyEntity competency = await FindEntityByCode(code);
        _context.Competencies.Remove(competency);
        await _context.SaveChangesAsync();
    }

    public async Task<Competency> FindByCode(string code)
    {
        return _mapper.Map<Competency>(await FindEntityByCode(code));
    }

    private async Task<CompetencyEntity> FindEntityByCode(string code)
    {
        CompetencyEntity? entity = await _context.Competencies
            .SingleOrDefaultAsync(x => x.Code == code);
        if (entity == null)
        {
            throw new EntityNotFoundException(nameof(Competency), code);
        }
        return entity;
    }
}
