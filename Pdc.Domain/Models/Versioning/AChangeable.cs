namespace Pdc.Domain.Models.Versioning;

public abstract class AChangeable
{
    public required Guid Id { get; set; }
    public required string Value { get; set; }
    public required List<ComplementaryInformation> ComplementaryInformations { get; set; }

    public virtual void SetVersion(ChangeRecord version)
    {
        ComplementaryInformations.ForEach(x => x.SetVersion(version));
    }
}
