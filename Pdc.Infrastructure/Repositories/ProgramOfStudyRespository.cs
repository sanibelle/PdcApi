using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.CourseFramework;
using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Entities.CourseFramework;
using Pdc.Infrastructure.Exceptions;

namespace Pdc.Infrastructure.Repositories;

public class ProgramOfStudyRespository : IProgramOfStudyRespository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ProgramOfStudyRespository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ProgramOfStudy>> GetAll()
    {
        List<ProgramOfStudyEntity> entities = await _context.ProgramOfStudies.ToListAsync();
        return _mapper.Map<List<ProgramOfStudy>>(entities);
    }

    public async Task<ProgramOfStudy> Add(ProgramOfStudy programOfStudy)
    {
        EntityEntry<ProgramOfStudyEntity> entity = await _context.ProgramOfStudies.AddAsync(_mapper.Map<ProgramOfStudyEntity>(programOfStudy));
        await _context.SaveChangesAsync();
        return _mapper.Map<ProgramOfStudy>(entity.Entity);
    }

    public async Task<ProgramOfStudy> Update(ProgramOfStudy programOfStudy)
    {
        ProgramOfStudyEntity entity = await FindEntityByCode(programOfStudy.Code);
        _mapper.Map(programOfStudy, entity);
        EntityEntry<ProgramOfStudyEntity> updatedEntity = _context.ProgramOfStudies.Update(entity);
        await _context.SaveChangesAsync();
        return _mapper.Map<ProgramOfStudy>(updatedEntity.Entity);
    }

    public async Task Delete(string code)
    {
        ProgramOfStudyEntity entity = await FindEntityByCode(code);
        _context.ProgramOfStudies.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<ProgramOfStudy> FindByCode(string code)
    {
        ProgramOfStudyEntity entity = await FindEntityByCode(code);
        return _mapper.Map<ProgramOfStudy>(entity);
    }

    private async Task<ProgramOfStudyEntity> FindEntityByCode(string code)
    {
        ProgramOfStudyEntity? program = await _context.ProgramOfStudies
            .Include(p => p.GeneralUnits)
            .Include(p => p.ComplementaryUnits)
            .Include(p => p.SpecificUnits)
            .Include(p => p.OptionnalUnits)
            .SingleOrDefaultAsync(x => x.Code == code);
        if (program == null)
        {
            throw new EntityNotFoundException(nameof(ProgramOfStudy), code);
        }
        return program;
    }
}
