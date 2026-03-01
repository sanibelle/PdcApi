using Pdc.Infrastructure.Entities.CourseFramework;
using Pdc.Infrastructure.Entities.MinisterialSpecification;

namespace Pdc.Infrastructure.Entities.Visitors;

public interface IChangeableVisitor<T>
{
    T Visit(CompetencyElementEntity competencyElement);
    T Visit(RealisationContextEntity realisationContext);
    T Visit(ContentElementEntity contentElementEntity);
    T Visit(PerformanceCriteriaEntity performanceCriteriaEntity);
}