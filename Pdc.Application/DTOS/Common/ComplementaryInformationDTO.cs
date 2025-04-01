namespace Pdc.Application.DTOS.Common;

public class ComplementaryInformationDTO
{
    public Guid? Id { get; set; }
    //UTILISATEUR
    /// <summary>
    /// Version à laquelle l'information a été ajoutée
    /// </summary>
    public int VersionNumber { get; set; }
    public DateTime ModifiedOn { get; set; }
    public required string Text { get; set; }

}