namespace Pdc.Domain.Entities.MinisterialSpecification;

public class MinisterialCompetency : Competency
{
    public IEnumerable<MinisterialCompetencyElement> CompetencyElements { get; set; } = new List<MinisterialCompetencyElement>();
}
