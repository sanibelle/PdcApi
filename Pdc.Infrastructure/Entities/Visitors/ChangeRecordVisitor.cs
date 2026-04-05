using Pdc.Infrastructure.Entities.Version;
using Pdc.Infrastructure.Entities.CourseFramework;
using Pdc.Infrastructure.Entities.MinisterialSpecification;

namespace Pdc.Infrastructure.Entities.Visitors;

public class ChangeRecordVisitor : IChangeableVisitor<ChangeRecordEntity?>
{
    public ChangeRecordEntity? Visit(CompetencyElementEntity? ce) => ce?.Competency?.ChangeRecord;

    public ChangeRecordEntity? Visit(RealisationContextEntity rc) => rc.Competency?.ChangeRecord;

    public ChangeRecordEntity? Visit(PerformanceCriteriaEntity pc) => Visit(pc.CompetencyElement);

    public ChangeRecordEntity? Visit(ContentElementEntity ce)
    {
        throw new NotImplementedException();
    }

}
