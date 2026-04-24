using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.Versioning;
using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Entities.Version;

namespace Pdc.Infrastructure.Repositories;

public class ChangeRecordRepository(AppDbContext context, IMapper mapper) : IChangeRecordRepository
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<ChangeRecord> AddChangeRecord(ChangeRecord changeRecord)
    {
        ChangeRecordEntity changeRecordEntity = _mapper.Map<ChangeRecordEntity>(changeRecord);
        ChangeRecordEntity? parentChangeRecord = null;
        // getting the parent first.
        if (changeRecordEntity.ParentChangeRecord?.Id != null)
        {
            parentChangeRecord = await FindEntityById(changeRecordEntity.ParentChangeRecord.Id.Value);
        }
        if (parentChangeRecord != null)
        {
            changeRecordEntity.ParentChangeRecord = parentChangeRecord;

        }
        EntityEntry<ChangeRecordEntity> entity = await _context.ChangeRecords.AddAsync(changeRecordEntity);
        if (parentChangeRecord != null)
        {
            parentChangeRecord.NextChangeRecord = entity.Entity;
        }
        await _context.SaveChangesAsync();
        return _mapper.Map<ChangeRecord>(entity.Entity);
    }

    public async Task<ChangeRecord> FindById(Guid changeRecordId)
    {
        ChangeRecordEntity? changeRecord = await FindEntityById(changeRecordId);
        if (changeRecord == null)
        {
            throw new NotFoundException(nameof(ChangeRecordEntity), changeRecordId);
        }
        return _mapper.Map<ChangeRecord>(changeRecord);
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

    public async Task<Guid> FindParentByChangeRecordId(Guid changeRecordId)
    {
        ChangeRecordEntity current = await _context.ChangeRecords
            .FirstAsync(x => x.Id == changeRecordId);

        if (current?.Id == null)
        {
            throw new NotFoundException(nameof(ChangeRecordEntity), changeRecordId);
        }
        while (current.ParentChangeRecordId != null)
        {
            current = await _context.ChangeRecords
                .FirstAsync(x => x.Id == current.ParentChangeRecordId);
        }

        if (current?.Id == null)
        {
            throw new NotFoundException(nameof(ChangeRecordEntity) + "ParentChangeRecord Id not found, value is null", changeRecordId);
        }
        return current.Id.Value;
    }

    public async Task<ChangeRecord> Publish(ChangeRecord changeRecord)
    {
        ChangeRecordEntity? changeRecordEntity = await FindEntityById(changeRecord.Id!.Value);
        if (changeRecordEntity == null)
        {
            throw new NotFoundException(nameof(ChangeRecordEntity), changeRecord.Id.Value);
        }
        if (changeRecordEntity.ChangeRecordNumber != changeRecord.ChangeRecordNumber)
        {
            throw new InvalidChangeRecordException($"The version of the change record with id {changeRecordEntity.Id!.Value} should have the same version : {changeRecordEntity.ChangeRecordNumber} but was set to {changeRecord.ChangeRecordNumber}.");
        }
        _mapper.Map(changeRecord, changeRecordEntity);
        _context.Update(changeRecordEntity);
        await _context.SaveChangesAsync();
        return _mapper.Map<ChangeRecord>(changeRecordEntity);
    }

    private async Task<ChangeRecordEntity?> FindEntityById(Guid changeRecordId)
    {
        return await _context.ChangeRecords.SingleOrDefaultAsync(x => x.Id == changeRecordId);

    }
}
