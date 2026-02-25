using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.Versioning;
using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Entities.Versioning;
using Pdc.Infrastructure.Entities.Visitors;

namespace Pdc.Infrastructure.Repositories;

public class ComplementaryInformationRepository : IComplementaryInformationRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ComplementaryInformationRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ComplementaryInformation> Add(ComplementaryInformation complementaryInformation, Guid changeableId)
    {
        EntityEntry<ComplementaryInformationEntity> entity = await _context.ComplementaryInformations.AddAsync(_mapper.Map<ComplementaryInformationEntity>(complementaryInformation));
        await _context.SaveChangesAsync();
        return _mapper.Map<ComplementaryInformation>(entity.Entity);
    }

    public async Task<ComplementaryInformation> Update(ComplementaryInformation complementaryInformation)
    {
        ComplementaryInformationEntity entity = await FindEntityById(complementaryInformation.Id);
        _mapper.Map(complementaryInformation, entity);
        EntityEntry<ComplementaryInformationEntity> updatedEntity = _context.ComplementaryInformations.Update(entity);
        await _context.SaveChangesAsync();
        return _mapper.Map<ComplementaryInformation>(updatedEntity.Entity);
    }

    public async Task Delete(Guid id)
    {
        ComplementaryInformationEntity entity = await FindEntityById(id);
        _context.ComplementaryInformations.Remove(entity);
        await _context.SaveChangesAsync();
    }

    private async Task<ComplementaryInformationEntity> FindEntityById(Guid? id)
    {
        var complementaryInformationEntity = await _context.ComplementaryInformations
            .SingleOrDefaultAsync(x => x.Id == id);
        if (complementaryInformationEntity == null || id == null)
        {
            throw new NotFoundException(nameof(ChangeableEntity), id == null ? "id is null" : id);
        }
        return complementaryInformationEntity;
    }

    public async Task<bool> ChangeableExists(Guid changeableId)
    {
        return await _context.Changeables
            .Where(x => x.Id == changeableId)
            .AnyAsync();
    }

    public async Task<ChangeRecord> GetVersionByChangeableId(Guid changeableId)
    {
        ChangeableEntity? changeable = await _context.Changeables
            .SingleOrDefaultAsync(x => x.Id == changeableId);

        if (changeable == null)
        {
            throw new NotFoundException(nameof(ChangeableEntity), changeableId);
        }
        ChangeRecordEntity? version = changeable.Accept(new CurrentVersionVisitor());

        if (changeable == null)
        {
            throw new NotFoundException(nameof(ChangeRecordEntity));
        }
        return _mapper.Map<ChangeRecord>(version);
    }

    public async Task<Guid> FindCreatedById(Guid complementaryInformationId)
    {
        Guid? id = await _context.ComplementaryInformations
            .Where(x => x.Id == complementaryInformationId)
            .Select(x => x.CreatedById)
            .SingleOrDefaultAsync();

        if (!id.HasValue)
        {
            throw new NotFoundException(nameof(ChangeRecordEntity) + "CreatedBy Id not found, value is null", complementaryInformationId);
        }
        return id.Value;
    }
}
