using Pdc.Domain.Entities.Common;

namespace Pdc.Domain.Entities.MinisterialSpecification;

public class MinisterialCompetency
{
    public Guid Id { get; set; }
    public required string Code { get; set; } //00SU
    /// <summary>
    /// Les unités choisies pour la compétence
    /// </summary>
    public Units? Units { get; set; }
    public required bool IsMandatory { get; set; } // true
    public required bool IsOptionnal { get; set; } // true
    public required string StatementOfCompetency { get; set; } // Effectuer le déploiement de serveurs intranet
    public required IList<MinisterialRealisationContext> RealisationContexts { get; set; } // Critères de performance liés à l’ensemble de la compétence
    public required IList<CompetencyElement> CompetencyElements { get; set; }
}
