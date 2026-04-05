using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.Versioning;
using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Entities.Version;
using Pdc.Infrastructure.Entities.Visitors;

namespace Pdc.Infrastructure.Repositories;

public class ComplementaryInformationRepository(AppDbContext context, IChangeRecordRepository changeRecordRepository, IMapper mapper) : IComplementaryInformationRepository
{
    private readonly AppDbContext _context = context;
    private readonly IChangeRecordRepository _changeRecordRepository = changeRecordRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ComplementaryInformation> Add(ComplementaryInformation complementaryInformation, Guid changeRecordId, Guid ChangeableId)
    {
        var toAdd = _mapper.Map<ComplementaryInformationEntity>(complementaryInformation);
        toAdd.Changeable = await _context.Changeables.SingleOrDefaultAsync(x => x.Id == ChangeableId) ?? throw new NotFoundException(nameof(ChangeableEntity), ChangeableId);
        toAdd.ChangeRecord = await _context.ChangeRecords.SingleOrDefaultAsync(x => x.Id == changeRecordId) ?? throw new NotFoundException(nameof(ChangeRecord), changeRecordId);
        EntityEntry<ComplementaryInformationEntity> entity = await _context.ComplementaryInformations.AddAsync(toAdd);
        await _context.SaveChangesAsync();
        return _mapper.Map<ComplementaryInformation>(entity.Entity);
    }

    public async Task<ComplementaryInformation> Update(ComplementaryInformation complementaryInformation, Guid changeRecordId)
    {
        ComplementaryInformationEntity entity = await FindEntityById(complementaryInformation.Id);
        _mapper.Map(complementaryInformation, entity);
        entity.ChangeRecord = await _context.ChangeRecords.SingleOrDefaultAsync(x => x.Id == changeRecordId) ?? throw new NotFoundException(nameof(ChangeRecord), changeRecordId);
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


    public async Task<bool> ChangeableExists(Guid changeableId)
    {
        return await _context.Changeables
            .Where(x => x.Id == changeableId)
            .AnyAsync();
    }

    public async Task<Guid> GetChangeRecordByChangeableId(Guid changeableId)
    {
        ChangeableEntity? changeable = await _context.Changeables
            .SingleOrDefaultAsync(x => x.Id == changeableId) ?? throw new NotFoundException(nameof(ChangeableEntity), changeableId);
        ChangeRecordEntity? changeRecord = changeable.Accept(new ChangeRecordVisitor());

        return changeRecord?.Id == null
            ? throw new NotFoundException(nameof(ChangeRecordEntity))
            : await _changeRecordRepository.FindParentByChangeRecordId(changeRecord.Id.Value);
    }

    public async Task<Guid> FindCreatedByByComplementaryInformationId(Guid complementaryInformationId)
    {
        Guid id = await _context.ComplementaryInformations
            .Where(x => x.Id == complementaryInformationId)
            .Select(x => x.CreatedById)
            .SingleOrDefaultAsync();

        if (Guid.Empty == id) // when complementaryInformation is not found, it retunrs a default Guid.
        {
            throw new NotFoundException(nameof(ComplementaryInformationEntity) + "CreatedBy Id not found, value is null", complementaryInformationId);
        }
        return id;
    }

    public async Task<ComplementaryInformation> FindById(Guid complementaryInformationId)
    {
        return _mapper.Map<ComplementaryInformation>(await FindEntityById(complementaryInformationId));
    }

    private async Task<ComplementaryInformationEntity> FindEntityById(Guid? id)
    {
        var complementaryInformationEntity = await _context.ComplementaryInformations
            .SingleOrDefaultAsync(x => x.Id == id);
        if (complementaryInformationEntity == null || id == null)
        {
            throw new NotFoundException(nameof(ComplementaryInformationEntity), id == null ? "id is null" : id);
        }
        return complementaryInformationEntity;
    }

}
