using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Pdc.Domain.Entities.MinisterialSpecification;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Infrastructure.Data;

namespace Pdc.Infrastructure.Repositories;

public class MinisterialCompetencyRepository : ICompetencyRespository
{
    private readonly AppDbContext _context;

    public MinisterialCompetencyRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Competency>> GetAll()
    {
        return await _context.Competencies.ToListAsync();
    }

    public async Task<Competency> Add(Competency competency)
    {
        EntityEntry<Competency> newMinisterialCompetency = await _context.Competencies.AddAsync(competency);
        await _context.SaveChangesAsync();
        return newMinisterialCompetency.Entity;
    }

    public async Task<Competency> Update(Competency competency)
    {
        EntityEntry<Competency> updatedCompetency = _context.Competencies.Update(competency);
        await _context.SaveChangesAsync();
        return updatedCompetency.Entity;
    }

    public async Task Delete(string code)
    {
        Competency competency = await FindByCode(code);
        _context.Competencies.Remove(competency);
        await _context.SaveChangesAsync();
    }

    public async Task<Competency> FindByCode(string code)
    {
        Competency? competency = await _context.Competencies
            .SingleOrDefaultAsync(x => x.Code == code);
        if (competency == null)
        {
            throw new EntityNotFoundException(nameof(Competency), code);
        }
        return competency;
    }
}
