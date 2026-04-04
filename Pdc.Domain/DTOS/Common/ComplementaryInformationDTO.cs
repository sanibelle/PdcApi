namespace Pdc.Domain.DTOS.Common;

public class ComplementaryInformationDTO
{
    public Guid? Id { get; set; }
    /// <summary>
    /// Version à laquelle l'information a été ajoutée
    /// </summary>
    public int? ChangeRecordNumber { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public DateTime? CreatedOn { get; set; }
    public UserDTO? CreatedBy { get; set; }
    public required string Text { get; set; }

}