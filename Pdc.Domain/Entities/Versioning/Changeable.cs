namespace Pdc.Domain.Entities.Versioning;

public class Changeable
{
    public required Guid Id { get; set; }
    public required string Value { get; set; }
    public required IEnumerable<ComplementaryInformation> ComplementaryInformations { get; set; }
    public string Discriminator { get; set; } = "Changeable";
}
