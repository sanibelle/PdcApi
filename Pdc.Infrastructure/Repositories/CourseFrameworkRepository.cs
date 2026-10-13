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
        ICourseFrameworkCompetencyRepository courseFrameworkCompetencyRepository,
        ILogger<CourseFrameworkRepository> logger,
        //[FromKeyedServices("tracked")] IChangeApplier<Changeable, CourseFrameworkEntity, CourseFrameworkChangeableEntity> trackedPerformanceCriteriaChangeApplier,
        [FromKeyedServices("untracked")] IChangeApplier<Changeable, CourseFrameworkEntity, CourseFrameworkChangeableEntity> untrackedourseFrameworkContextChangeApplier)
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
            addedCourseFrameworkEntity.Entity.Code = await untrackedourseFrameworkContextChangeApplier.Add(changeRecord,
                addedCourseFrameworkEntity.Entity,
                courseFramework.Code,
                tracker);
            addedCourseFrameworkEntity.Entity.Name = await untrackedourseFrameworkContextChangeApplier.Add(changeRecord,
                addedCourseFrameworkEntity.Entity,
                courseFramework.Name,
                tracker);
            addedCourseFrameworkEntity.Entity.Semester = await untrackedourseFrameworkContextChangeApplier.Add(changeRecord,
                addedCourseFrameworkEntity.Entity,
                courseFramework.Semester,
                tracker);
            addedCourseFrameworkEntity.Entity.TheoryHours = await untrackedourseFrameworkContextChangeApplier.Add(changeRecord,
                addedCourseFrameworkEntity.Entity,
                courseFramework.Weighting.TheoryHours!,
                tracker);
            addedCourseFrameworkEntity.Entity.LaboratoryHours = await untrackedourseFrameworkContextChangeApplier.Add(changeRecord,
                addedCourseFrameworkEntity.Entity,
                courseFramework.Weighting.LaboratoryHours!,
                tracker);
            addedCourseFrameworkEntity.Entity.PersonnalWorkHours = await untrackedourseFrameworkContextChangeApplier.Add(changeRecord,
                addedCourseFrameworkEntity.Entity,
                courseFramework.Weighting.PersonnalWorkHours!,
                tracker);

            var toAdd = courseFramework.Competencies
                .Where(c => !addedCourseFrameworkEntity.Entity.CourseFrameworkCompetencies.Any(e => e.Competency.Code == c.Code))
                .ToList();

            await context.SaveChangesAsync(); // one last save to track the last field added to CourseFramework

            foreach (var code in toAdd.Select(x => x.Code))
            {
                await courseFrameworkCompetencyRepository.Link(code, addedCourseFrameworkEntity.Entity.Id!.Value);
            }

            await LoadCourseFrameworkCompetencies(addedCourseFrameworkEntity.Entity);

            logger.LogInformation($"Before commit transaction with code {addedCourseFrameworkEntity.Entity.Code} to database");

            await transaction.CommitAsync();
            logger.LogInformation($"Transaction success with code {addedCourseFrameworkEntity.Entity.Code} ");
            return mapper.Map<CourseFramework>(addedCourseFrameworkEntity.Entity);
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    private async Task LoadCourseFrameworkCompetencies(CourseFrameworkEntity addedCourseFrameworkEntity)
    {
        var competenciesCollection = context.Entry(addedCourseFrameworkEntity)
            .Collection(x => x.CourseFrameworkCompetencies);
        await competenciesCollection.Query()
            .Include(cfc => cfc.Competency)
            .LoadAsync();
    }

    public async Task<bool> ExistsEntityByCode(string courseFrameworkCode)
    {
        return await context.CourseFrameworks.AnyAsync(x => x.Code.Value == courseFrameworkCode);
    }

    public async Task<CourseFramework> FindById(Guid id)
    {
        CourseFrameworkEntity res = await FindEntityById(id);
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
        CourseFrameworkEntity courseFramekwork = await FindEntityById(courseFramekworkId);
        context.Remove(courseFramekwork);
        await context.SaveChangesAsync();

    }

    public async Task<CourseFramework> UpdateUntrackedChangeable(CourseFramework courseFramework)
    {
        using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            IChangeTracker tracker = new NoOpChangeDetailsTracker();
            logger.LogInformation($"Updating courseFramework {courseFramework.Code} with id {courseFramework.Id}");
            CourseFrameworkEntity existingCourseFramework = await FindEntityById(courseFramework.Id!.Value, false);

            await UpdateCourseFrameworkAndChilds(courseFramework, tracker, existingCourseFramework, untrackedourseFrameworkContextChangeApplier);
            logger.LogInformation($"Commiting courseFramework {courseFramework.Code} with id {courseFramework.Id}");
            await transaction.CommitAsync();
            logger.LogInformation($"Comitted courseFramework {courseFramework.Code} with id {courseFramework.Id}");
            return await FindById(courseFramework.Id!.Value);
        }
        catch
        {
            await transaction.RollbackAsync();
            logger.LogError($"Failed to updated the courseFramework {courseFramework.Code} with id {courseFramework.Id}");
            throw;
        }
    }

    private async Task<CourseFrameworkEntity> FindEntityById(Guid id, bool asNoTracking = true)
    {
        IQueryable<CourseFrameworkEntity> query = context.CourseFrameworks;
        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }

        return await query
            .AsNoTracking()
            .Include(x => x.Code)
            .Include(x => x.Name)
            .Include(x => x.Semester)
            .Include(x => x.TheoryHours)
            .Include(x => x.LaboratoryHours)
            .Include(x => x.PersonnalWorkHours)
            .Include(x => x.CourseFrameworkCompetencies)
                .ThenInclude(y => y.Competency)
            .SingleOrDefaultAsync(x => x.Id == id)
            ?? throw new NotFoundException("CourseFramework not found", id);
    }

    private async Task UpdateCourseFrameworkAndChilds(CourseFramework courseFramework, IChangeTracker tracker, CourseFrameworkEntity existingCourseFramework, IChangeApplier<Changeable, CourseFrameworkEntity, CourseFrameworkChangeableEntity> realisationContextChangeApplier)
    {
        mapper.Map(courseFramework, existingCourseFramework);
        var changeRecord = courseFramework.ChangeRecord ?? throw new NotFoundException("Change record not found", courseFramework.Id.HasValue ? courseFramework.Id.Value : "id is null");
        await context.SaveChangesAsync();
        await realisationContextChangeApplier.Update(changeRecord, courseFramework.Code, tracker);
        await realisationContextChangeApplier.Update(changeRecord, courseFramework.Name, tracker);
        await realisationContextChangeApplier.Update(changeRecord, courseFramework.Semester, tracker);
        await realisationContextChangeApplier.Update(changeRecord, courseFramework.Weighting.TheoryHours!, tracker);
        await realisationContextChangeApplier.Update(changeRecord, courseFramework.Weighting.LaboratoryHours!, tracker);
        await realisationContextChangeApplier.Update(changeRecord, courseFramework.Weighting.PersonnalWorkHours!, tracker);
        var toAdd = courseFramework.Competencies
            .Where(c => !existingCourseFramework.CourseFrameworkCompetencies.Any(e => e.Competency.Code == c.Code))
            .ToList();
        var toRemove = existingCourseFramework.CourseFrameworkCompetencies
            .Where(c => !courseFramework.Competencies.Any(e => e.Code == c.Competency.Code))
            .ToList();

        foreach (var code in toAdd.Select(x => x.Code))
        {
            await courseFrameworkCompetencyRepository.Link(code, existingCourseFramework.Id!.Value);
        }

        foreach (var code in toRemove.Select(x => x.Competency.Code))
        {
            await courseFrameworkCompetencyRepository.Unlink(code, existingCourseFramework.Id!.Value);
        }

        await LoadCourseFrameworkCompetencies(existingCourseFramework);
        await context.SaveChangesAsync();
    }
}
