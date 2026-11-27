using AutoMapper;
using Pdc.Application.DTOS.CourseFramework;
using Pdc.Domain.Exceptions;
using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Interfaces.UseCases.CourseFramework;
using System.Text.RegularExpressions;

namespace Pdc.Application.UseCases;

public class GetCourseFrameworkById(ICourseFrameworkRepository courseFrameworksRepository, IMapper mapper) : IGetCourseFrameworkByIdUseCase
{
    public async Task<TrackedCourseFrameworkDTO> Execute(string code)
    {
        if (!Regex.IsMatch(code, @"^[0-9]{3}-[A-Z0-9]{3}-[A-Z]{2}$"))
        {
            throw new InvalidCodeException($"Code ({code}) does not respect the required format");
        }

        var courseFramework = await courseFrameworksRepository.FindByCode(code);
        return mapper.Map<TrackedCourseFrameworkDTO>(courseFramework);
    }
}
