using Pdc.Domain.Entities.Common;

namespace Pdc.Domain.Entities.MinisterialSpecification;

public class MinisterialCompetency
{
    public Guid Id { get; set; }
    public required string Code { get; set; } //00SU
    public required string StatementOfCompetency { get; set; } // Effectuer le déploiement de serveurs intranet
    public required IList<MinisterialRealisationContext> RealisationContexts { get; set; }
    public required IList<CompetencyElement> CompetencyElements { get; set; }
}
