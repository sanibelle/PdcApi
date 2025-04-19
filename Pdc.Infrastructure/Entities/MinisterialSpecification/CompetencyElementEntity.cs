using Pdc.Infrastructure.Entities.Identity;
using Pdc.Infrastructure.Entities.Versioning;

namespace Pdc.Infrastructure.Entities.MinisterialSpecification;

public class CompetencyElementEntity : ChangeableEntity
{
    public ICollection<PerformanceCriteriaEntity> PerformanceCriterias { get; set; } = new List<PerformanceCriteriaEntity>();
    public required int Position { get; set; }

    internal override void SetCreatedBy(IdentityUserEntity createdBy)
    {
        base.SetCreatedBy(createdBy);
        PerformanceCriterias.ToList().ForEach(x => x.SetCreatedBy(createdBy));
    }
}
