using AutoMapper;
using FluentValidation;
using Pdc.Application.DTOS.CourseFramework;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Interfaces.UseCases.CourseFramework;
using Pdc.Domain.Models.CourseFramework;
using Pdc.Domain.Models.Security;


namespace Pdc.Application.UseCases;

public class UpdateDraftV1CourseFramework(ICourseFrameworkRepository courseFrameworkRepository,
                           IMapper mapper,
                           IValidator<TrackedCourseFrameworkDTO> validator) : IUpdateDraftV1CourseFramekworkUseCase
{
    public async Task<TrackedCourseFrameworkDTO> Execute(Guid courseFrameworkId, TrackedCourseFrameworkDTO updateCourseFrameworkDto, User currentUser)
    {
        var validationResult = await validator.ValidateAsync(updateCourseFrameworkDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        CourseFramework courseFrameworkToUpdate = await courseFrameworkRepository.FindById(courseFrameworkId);
        if (!courseFrameworkToUpdate.IsDraftAndV1OrNull())
        {
            throw new InvalidChangeRecordException("Cannot update a non-draft course framework with change record greater than 1.");
        }
        mapper.Map(updateCourseFrameworkDto, courseFrameworkToUpdate);
        // On prend la version actuelle et on l'assigne à tous les objets qui ont une version.
        courseFrameworkToUpdate.SetChangeRecordOnUntracked(courseFrameworkToUpdate.ChangeRecord!);
        courseFrameworkToUpdate.SetCreatedByOnUntracked(currentUser);
        courseFrameworkToUpdate.SetCreatedOnOnUntracked();
        CourseFramework updatedCourseFramework = await courseFrameworkRepository.UpdateUntrackedChangeable(courseFrameworkToUpdate);
        return mapper.Map<TrackedCourseFrameworkDTO>(updatedCourseFramework);
    }
}