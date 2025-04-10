namespace Pdc.Infrastructure.Entities.Versioning;

public class ComplementaryInformationEntity
{
    public Guid Id { get; set; }
    public required ChangeableEntity Changeable { get; set; }
    //UTILISATEUR
    /// <summary>
    /// Version à laquelle l'information a été ajoutée
    /// </summary>
    public required ChangeRecordEntity WrittenOnVersion { get; set; }
    public DateTime ModifiedOn { get; set; }
    public required string Text { get; set; }
}
