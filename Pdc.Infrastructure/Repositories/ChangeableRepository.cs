using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.Versioning;
using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Entities.Version;

namespace Pdc.Infrastructure.Repositories;

public class ChangeableRepository(AppDbContext context, IMapper mapper) : IChangeableRepository
{
    public async Task<Changeable> FindById(Guid id)
    {
        ChangeableEntity changeableEntity = await context.Changeables.SingleOrDefaultAsync(x => x.Id == id) ?? throw new NotFoundException(nameof(Changeable), id);
        return mapper.Map<Changeable>(changeableEntity);
    }

    public async Task<Changeable> Update(Changeable changeable)
    {
        ChangeableEntity changeableEntity = await context.Changeables.SingleOrDefaultAsync(x => x.Id == changeable.Id) ?? throw new NotFoundException(nameof(Changeable), changeable.Id!);
        changeableEntity.Value = changeable.Value;
        await context.SaveChangesAsync();
        return mapper.Map<Changeable>(changeableEntity);
    }
}
