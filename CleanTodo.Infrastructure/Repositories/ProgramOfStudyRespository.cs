using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Pdc.Domain.Entities.CourseFramework;
using Pdc.Domain.Interfaces.Repositories;

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

    public async Task<ProgramOfStudy> Add(ProgramOfStudy todo)
    {
        EntityEntry<ProgramOfStudy> newProgramOfStudy = await _context.ProgramOfStudies.AddAsync(todo);
        await _context.SaveChangesAsync();
        return newProgramOfStudy.Entity;
    }

    public async Task Delete(Guid id)
    {
        ProgramOfStudy todo = await FindById(id);
        _context.ProgramOfStudies.Remove(todo);
        await _context.SaveChangesAsync();
    }

    public async Task<ProgramOfStudy> FindById(Guid id)
    {
        return await _context.ProgramOfStudies
            .Where(x => x.Id == id)
            .SingleAsync();
    }
}
