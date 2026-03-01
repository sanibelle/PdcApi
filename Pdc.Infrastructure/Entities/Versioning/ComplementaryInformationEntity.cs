using Pdc.Infrastructure.Entities.Identity;

namespace Pdc.Infrastructure.Entities.Versioning;

public class ComplementaryInformationEntity
{
    public Guid? Id { get; set; }
    public virtual ChangeableEntity? Changeable { get; set; }
    /// <summary>
    /// Version à laquelle l'information a été ajoutée
    /// </summary>
    public virtual ChangeRecordEntity? WrittenOnVersion { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public DateTime CreatedOn { get; set; }
    public virtual IdentityUserEntity? CreatedBy { get; set; }
    public Guid? CreatedById { get; set; }
    public required string Text { get; set; }
}
