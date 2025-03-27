namespace Pdc.Domain.Entities.Versioning;

public class ComplementaryInformation  // Informations supplémentaires comme des notes. Utilisé comme 4ieme colonne des éléments de contenu
{
    public Guid Id { get; set; }
    public required AChangeable Changeable { get; set; }
    //UTILISATEUR
    /// <summary>
    /// Version à laquelle l'information a été ajoutée
    /// </summary>
    public required ChangeRecord WrittenOnVersion { get; set; }
    public DateTime ModifiedOn { get; set; }
    public required string Text { get; set; }
}
