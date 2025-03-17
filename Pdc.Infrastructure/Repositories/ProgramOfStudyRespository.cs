using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Pdc.Domain.Entities.CourseFramework;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Infrastructure.Data;

namespace Pdc.Infrastructure.Repositories;

public class ProgramOfStudyRespository : IProgramOfStudyRespository
{
    private readonly AppDbContext _context;

    public ProgramOfStudyRespository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProgramOfStudy>> GetAll()
    {
        return await _context.ProgramOfStudies.ToListAsync();
    }

    public async Task<ProgramOfStudy> Add(ProgramOfStudy programOfStudy)
    {
        EntityEntry<ProgramOfStudy> newProgramOfStudy = await _context.ProgramOfStudies.AddAsync(programOfStudy);
        await _context.SaveChangesAsync();
        return newProgramOfStudy.Entity;
    }

    public async Task<ProgramOfStudy> Update(ProgramOfStudy programOfStudy)
    {
        EntityEntry<ProgramOfStudy> updatedProgram = _context.ProgramOfStudies.Update(programOfStudy);
        await _context.SaveChangesAsync();
        return updatedProgram.Entity;
    }

    public async Task Delete(Guid id)
    {
        ProgramOfStudy programOfStudy = await FindById(id);
        _context.ProgramOfStudies.Remove(programOfStudy);
        await _context.SaveChangesAsync();
    }

    public async Task<ProgramOfStudy> FindById(Guid id)
    {
        ProgramOfStudy? program = await _context.ProgramOfStudies
            .Include(p => p.GeneralUnits)
            .Include(p => p.ComplementaryUnits)
            .Include(p => p.SpecificUnits)
            .Include(p => p.OptionnalUnits)
            .SingleOrDefaultAsync(x => x.Id == id);
        if (program == null)
        {
            throw new EntityNotFoundException(nameof(ProgramOfStudy), id);
        }
        return program;
    }
}
