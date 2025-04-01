namespace Pdc.Domain.Models.Versioning;

public abstract class AChangeable
{
    public required Guid Id { get; set; }
    public required string Value { get; set; }
    public required IEnumerable<ComplementaryInformation> ComplementaryInformations { get; set; }
}
