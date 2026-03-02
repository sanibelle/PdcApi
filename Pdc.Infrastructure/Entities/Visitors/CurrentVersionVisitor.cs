using Pdc.Infrastructure.Entities.CourseFramework;
using Pdc.Infrastructure.Entities.MinisterialSpecification;
using Pdc.Infrastructure.Entities.Versioning;

namespace Pdc.Infrastructure.Entities.Visitors;

public class CurrentVersionVisitor : IChangeableVisitor<ChangeRecordEntity?>
{
    public ChangeRecordEntity? Visit(CompetencyElementEntity? ce) => ce?.Competency?.CurrentVersion;

    public ChangeRecordEntity? Visit(RealisationContextEntity rc) => rc.Competency?.CurrentVersion;

    public ChangeRecordEntity? Visit(PerformanceCriteriaEntity pc) => Visit(pc.CompetencyElement);

    public ChangeRecordEntity? Visit(ContentElementEntity ce)
    {
        throw new NotImplementedException();
    }

}
