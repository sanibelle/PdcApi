using Pdc.Domain.Models.Security;

namespace Pdc.Domain.Models.Versioning;

public class ComplementaryInformation  // Informations supplémentaires comme des notes. Utilisé comme 4ieme colonne des éléments de contenu
{
    public Guid Id { get; set; }
    /// <summary>
    /// Version à laquelle l'information a été ajoutée
    /// </summary>
    public required ChangeRecord WrittenOnVersion { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public required DateTime CreatedOn { get; set; }
    public required string Text { get; set; }
    public required User CreatedBy { get; set; }
    public void SetVersion(ChangeRecord version)
    {
        WrittenOnVersion = version;
    }

    internal void SetCreatedBy(User createdBy)
    {
        CreatedBy = createdBy;
    }

    public ComplementaryInformation()
    {
        Id = Guid.NewGuid();
        CreatedOn = DateTime.Now;
    }
}
