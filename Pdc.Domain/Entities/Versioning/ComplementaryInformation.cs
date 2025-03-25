namespace Pdc.Domain.Entities.Versioning;

public class ComplementaryInformation  // Informations supplémentaires comme des notes. Utilisé comme 4ieme colonne des éléments de contenu
{
    public Guid Id { get; set; }
    public required Changeable Changeable { get; set; }
    //UTILISATEUR
    public required ChangeRecord WrittenOnVersion { get; set; }
    public DateTime ModifiedOn { get; set; }
    public required string Text { get; set; }
}
