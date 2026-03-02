using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Entities.Versioning;

namespace Pdc.Infrastructure.Repositories;

public class VersionRepository(AppDbContext context, IMapper mapper) : IVersionRepository
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;

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

    public async Task<Guid> FindParentByVersionId(Guid versionId)
    {
        ChangeRecordEntity current = await _context.ChangeRecords
            .FirstAsync(x => x.Id == versionId);

        if (current?.Id == null)
        {
            throw new NotFoundException(nameof(ChangeRecordEntity), versionId);
        }
        while (current.ParentVersionId != null)
        {
            current = await _context.ChangeRecords
                .FirstAsync(x => x.Id == current.ParentVersionId);
        }

        if (current?.Id == null)
        {
            throw new NotFoundException(nameof(ChangeRecordEntity) + "ParentVersion Id not found, value is null", versionId);
        }
        return current.Id.Value;
    }

}
