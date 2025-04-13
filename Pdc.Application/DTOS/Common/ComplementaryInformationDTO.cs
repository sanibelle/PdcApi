using Pdc.Infrastructure.Entities.Identity;

namespace Pdc.Application.DTOS.Common;

public class ComplementaryInformationDTO
{
    public Guid? Id { get; set; }
    public IdentityUserEntity? ModifiedBy { get; set; }
    /// <summary>
    /// Version à laquelle l'information a été ajoutée
    /// </summary>
    public int? WrittenOnVersion { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public required DateTime CreatedOn { get; set; }
    public required UserDTO CreatedBy { get; set; }
    public required string Text { get; set; }

}