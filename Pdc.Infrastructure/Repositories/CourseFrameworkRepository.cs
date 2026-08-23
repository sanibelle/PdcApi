using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Models.CourseFramework;
using Pdc.Domain.Models.Versioning;
using Pdc.Infrastructure.ChangeTracker;
using Pdc.Infrastructure.Data;
using Pdc.Infrastructure.Entities.CourseFramework;
using Pdc.Infrastructure.Interfaces;

namespace Pdc.Infrastructure.Repositories;

internal class CourseFrameworkRepository(
        AppDbContext context,
        IMapper mapper,
        IChangeRecordRepository changeRecordRepository,
        ILogger<CourseFrameworkRepository> logger,
        //[FromKeyedServices("tracked")] IChangeApplier<Changeable, CourseFrameworkEntity, CourseFrameworkChangeableEntity> trackedPerformanceCriteriaChangeApplier,
        [FromKeyedServices("untracked")] IChangeApplier<Changeable, CourseFrameworkEntity, CourseFrameworkChangeableEntity> untrackedRealisationContextChangeApplier)
    : ICourseFrameworkRepository
{
    public async Task<CourseFramework> Add(CourseFramework courseFramework)
    {
        using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            IChangeTracker tracker = new NoOpChangeDetailsTracker();
            logger.LogInformation($"Adding course framework with code {courseFramework.Code} to program of study {courseFramework.ProgramOfStudyCode}");
            ChangeRecord changeRecord = await changeRecordRepository.AddChangeRecord(courseFramework.ChangeRecord!);

            CourseFrameworkEntity courseFrameworkEntity = mapper.Map<CourseFrameworkEntity>(courseFramework);
            courseFrameworkEntity.ChangeRecordId = changeRecord.Id;
            courseFrameworkEntity.ProgramOfStudyCode = courseFramework.ProgramOfStudyCode;

            logger.LogInformation($"Mapped course framework to entity with code {courseFrameworkEntity.Code} for program of study {courseFramework.Code}");
            EntityEntry<CourseFrameworkEntity> addedCourseFrameworkEntity = await context.CourseFrameworks.AddAsync(courseFrameworkEntity);
            await context.SaveChangesAsync();

            logger.LogInformation($"Saved course framework entity with code {addedCourseFrameworkEntity.Entity.Code} to database for program of study {courseFramework.Code}");
            await untrackedRealisationContextChangeApplier.Add(changeRecord, addedCourseFrameworkEntity.Entity, courseFramework.Code, tracker);
            await untrackedRealisationContextChangeApplier.Add(changeRecord, addedCourseFrameworkEntity.Entity, courseFramework.Name, tracker);
            await untrackedRealisationContextChangeApplier.Add(changeRecord, addedCourseFrameworkEntity.Entity, courseFramework.Weighting.TheoryHours!, tracker);
            await untrackedRealisationContextChangeApplier.Add(changeRecord, addedCourseFrameworkEntity.Entity, courseFramework.Weighting.LaboratoryHours!, tracker);
            await untrackedRealisationContextChangeApplier.Add(changeRecord, addedCourseFrameworkEntity.Entity, courseFramework.Weighting.PersonnalWorkHours!, tracker);

            logger.LogInformation($"Before commit transaction with code {addedCourseFrameworkEntity.Entity.Code} to database");
            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            logger.LogInformation($"Transaction success with code {addedCourseFrameworkEntity.Entity.Code} ");
            return mapper.Map<CourseFramework>(courseFrameworkEntity);
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<bool> ExistsEntityByCode(string courseFrameworkCode)
    {
        return await context.CourseFrameworks.AnyAsync(x => x.Code.Value == courseFrameworkCode);
    }

    public async Task<CourseFramework> FindById(Guid id)
    {
        CourseFrameworkEntity res = await context.CourseFrameworks.SingleOrDefaultAsync(x => x.Id == id) ?? throw new NotFoundException("CourseFramework not found", id);
        return mapper.Map<CourseFramework>(res);
    }

    public async Task<CourseFramework> FindByCode(string code)
    {
        CourseFrameworkEntity res = await context.CourseFrameworks.SingleOrDefaultAsync(x => x.Code.Value == code) ?? throw new NotFoundException("CourseFramework not found", code);
        return mapper.Map<CourseFramework>(res);
    }

    public async Task<IList<CourseFramework>> GetByProgramOfStudy(string code)
    {
        List<CourseFrameworkEntity> res = await context.CourseFrameworks
            .Where(x => x.ProgramOfStudy.Code == code)
            .ToListAsync();
        return mapper.Map<IList<CourseFramework>>(res);
    }

    public async Task DeleteById(Guid courseFramekworkId)
    {
        CourseFrameworkEntity courseFramekwork = await context.CourseFrameworks.SingleOrDefaultAsync(x => x.Id == courseFramekworkId) ?? throw new NotFoundException("CourseFramework not found", courseFramekworkId);
        context.Remove(courseFramekwork);
        await context.SaveChangesAsync();

    }
}
