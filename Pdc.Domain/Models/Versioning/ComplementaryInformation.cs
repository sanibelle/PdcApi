using Pdc.Domain.Models.Security;

namespace Pdc.Domain.Models.Versioning;

public class ComplementaryInformation  // Informations supplémentaires comme des notes. Utilisé comme 4ieme colonne des éléments de contenu
{
    public Guid? Id { get; set; }
    /// <summary>
    /// Version à laquelle l'information a été ajoutée
    /// </summary>
    public ChangeRecord? WrittenOnVersion { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public required DateTime CreatedOn { get; set; }
    public required string Text { get; set; }
    public required User CreatedBy { get; set; }
    public void SetVersionOnUntracked(ChangeRecord version)
    {
        if (WrittenOnVersion == null)
        {
            WrittenOnVersion = version;
        }
    }

    public void SetCreatedByOnUntracked(User user)
    {
        if (CreatedBy == null)
        {
            CreatedBy = user;
        }
    }

    public ComplementaryInformation()
    {
        Id = Guid.NewGuid();
        CreatedOn = DateTime.UtcNow;
    }
}
