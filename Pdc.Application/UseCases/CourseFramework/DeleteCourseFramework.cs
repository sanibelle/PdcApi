using Pdc.Domain.Interfaces.Repositories;
using Pdc.Domain.Interfaces.UseCases.CourseFramework;

namespace Pdc.Application.UseCases;

public class DeleteCourseFramework(ICourseFrameworkRepository courseFrameworksRepository) : IDeleteCourseFrameworkUseCase
{
    public async Task Execute(Guid courseFramekworkId)
    {
        await courseFrameworksRepository.DeleteById(courseFramekworkId);
    }
}
