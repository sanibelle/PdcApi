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

public class ComplementaryInformationRepository(AppDbContext context, IVersionRepository versionRepository, IMapper mapper) : IComplementaryInformationRepository
{
    private readonly AppDbContext _context = context;
    private readonly IVersionRepository _versionRepository = versionRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ComplementaryInformation> Add(ComplementaryInformation complementaryInformation, Guid versionId)
    {
        var toAdd = _mapper.Map<ComplementaryInformationEntity>(complementaryInformation);
        toAdd.WrittenOnVersion = await _context.ChangeRecords.SingleOrDefaultAsync(x => x.Id == versionId);
        EntityEntry<ComplementaryInformationEntity> entity = await _context.ComplementaryInformations.AddAsync(toAdd);
        await _context.SaveChangesAsync();
        return _mapper.Map<ComplementaryInformation>(entity.Entity);
    }

    public async Task<ComplementaryInformation> Update(ComplementaryInformation complementaryInformation, Guid versionId)
    {
        ComplementaryInformationEntity entity = await FindEntityById(complementaryInformation.Id);
        _mapper.Map(complementaryInformation, entity);
        entity.WrittenOnVersion = await _context.ChangeRecords.SingleOrDefaultAsync(x => x.Id == versionId);
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

    public async Task<Guid> GetVersionByChangeableId(Guid changeableId)
    {
        ChangeableEntity? changeable = await _context.Changeables
            .SingleOrDefaultAsync(x => x.Id == changeableId) ?? throw new NotFoundException(nameof(ChangeableEntity), changeableId);
        ChangeRecordEntity? version = changeable.Accept(new CurrentVersionVisitor());

        return version?.Id == null
            ? throw new NotFoundException(nameof(ChangeRecordEntity))
            : await _versionRepository.FindParentByVersionId(version.Id.Value);
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
            throw new NotFoundException(nameof(ChangeableEntity), id == null ? "id is null" : id);
        }
        return complementaryInformationEntity;
    }
}
