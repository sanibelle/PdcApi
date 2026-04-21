using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pdc.Domain.Enums;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Infrastructure.Data;

namespace Pdc.Infrastructure.Repositories;

public class ChangeDetailsRepository(AppDbContext context, IMapper mapper) : IChangeDetailsRepository
{
    public async Task<List<Guid>> FindDeletedChangeableIdByChangeRecordId(Guid changeRecordId)
    {
        List<Guid> ids = await context.ChangeDetails
            .Where(x => x.ChangeRecordId == changeRecordId)
            .Where(x => x.ChangeType == ChangeType.Delete)
            .Select(x => x.Changeable.Id!.Value)
            .ToListAsync();

        return ids;
    }
}
