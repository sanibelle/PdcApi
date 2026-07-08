using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Pdc.Domain.Enums;
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
        if (changeRecordEntity.ParentChangeRecordId.HasValue)
        {
            parentChangeRecord = await FindEntityById(changeRecordEntity.ParentChangeRecordId.Value);
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
        if (changeRecord.RootId.HasValue)
        {
            changeRecordEntity.RootId = changeRecord.RootId.Value;
        }
        else
        {
            changeRecordEntity.Root = changeRecordEntity;
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

    public async Task<Guid> FindIdByRootIdAndNumber(int changeRecordNumber, Guid rootId)
    {
        Guid? id = await _context.ChangeRecords
                .Where(x => x.ChangeRecordNumber == changeRecordNumber && x.RootId == rootId)
                .Select(x => x.Id)
                .SingleAsync();
        if (!id.HasValue)
        {
            throw new NotFoundException(nameof(ChangeRecordEntity), $"Change record with number {changeRecordNumber} and rootId {rootId} not found.");
        }
        return id.Value;
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

    public async Task<Guid> FindLatestChangeRecordIdByChangeRecordId(Guid rootId)
    {
        return await _context.ChangeRecords
            .Where(x => x.RootId == rootId)
            .OrderByDescending(x => x.ChangeRecordNumber)
            .Select(x => x.Id)
            .FirstOrDefaultAsync()
            ??
            throw new NotFoundException(nameof(ChangeRecordEntity) + "Latest ChangeRecord by rootId not found, value is null", rootId);
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

    public async IAsyncEnumerable<ChangeDetail> FindNextChangeDetailsByChangeType(List<ChangeDetail> updatedChangeDetails, ChangeType changeType, Guid rootId)
    {
        foreach (ChangeDetail cd in updatedChangeDetails)
        {
            ChangeDetailEntity? latestChangeDetail = await _context.ChangeDetails.Where(x => x.ChangeableId == cd.Changeable.Id)
                .Where(x => x.ChangeType == changeType)
                .Where(x => x.Id != cd.Id)
                .Where(x => x.ChangeRecord.ChangeRecordNumber >= cd.ChangeRecord.ChangeRecordNumber)
                .Where(x => x.ChangeRecord.RootId == rootId)
                .Include(x => x.ChangeRecord)
                .Include(x => x.Changeable)
                .OrderBy(x => x.ChangeRecord.CreatedOn) // oldest first
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if (latestChangeDetail != null)
            {
                yield return _mapper.Map<ChangeDetail>(latestChangeDetail);
            }
        }
    }

    public async Task<List<Guid>> FindNextChangeDetailsByChangeRecordNumber(int changeRecordNumber, ChangeType changeType, Guid rootId)
    {
        return await _context.ChangeDetails
            .Where(x => x.ChangeRecord.ChangeRecordNumber > changeRecordNumber)
            .Where(x => x.ChangeRecord.RootId == rootId)
            .Where(x => x.ChangeType == changeType)
            .AsNoTracking()
            .Select(x => x.ChangeableId)
            .ToListAsync();
    }
}
