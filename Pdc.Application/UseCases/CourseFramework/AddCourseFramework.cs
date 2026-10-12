using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Pdc.Application.DTOS.CourseFramework;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Interfaces.UseCases.CourseFramework;
using Pdc.Domain.Models.CourseFramework;
using Pdc.Domain.Models.MinisterialSpecification;
using Pdc.Domain.Models.Security;
using Pdc.Domain.Models.Versioning;

namespace Pdc.Application.UseCases;

public class AddCourseFramework(
    ICourseFrameworkRepository courseFrameworksRepository,
    ICompetencyRepository competencyRepository,
    IMapper mapper,
    IValidator<CreateCourseFrameworkDTO> validator) : IAddCourseFrameworkUseCase
{
    public async Task<TrackedCourseFrameworkDTO> Execute(string programOfStudyCode, CreateCourseFrameworkDTO courseFrameworkDto, User currentUser)
    {
        ValidationResult validationResult = await validator.ValidateAsync(courseFrameworkDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        await ThrowIfDuplicateCode(courseFrameworkDto.Code);
        List<MinisterialCompetency> competencies = await competencyRepository.FindByCodes(courseFrameworkDto.CompetencyCodes);
        if (competencies.Count() != courseFrameworkDto.CompetencyCodes.Length)
        {
            throw new ValidationException("Could not find all related competency codes");
        }

        CourseFramework courseFramwork = mapper.Map<CourseFramework>(courseFrameworkDto);
        courseFramwork.SetChangeRecordOnUntracked(new ChangeRecord(currentUser));
        courseFramwork.SetCreatedByOnUntracked(currentUser);
        courseFramwork.SetCreatedOnOnUntracked();
        courseFramwork.ProgramOfStudyCode = programOfStudyCode;
        courseFramwork.Competencies = competencies;
        CourseFramework savedCourseFramework = await courseFrameworksRepository.Add(courseFramwork);

        return mapper.Map<TrackedCourseFrameworkDTO>(savedCourseFramework);

    }

    private async Task ThrowIfDuplicateCode(string courseFrameworkCode)
    {
        if (await courseFrameworksRepository.ExistsEntityByCode(courseFrameworkCode))
        {
            throw new DuplicateException($"The courseFramework with the code {courseFrameworkCode} already exists.");
        }
    }
}
