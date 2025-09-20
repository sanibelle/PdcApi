using Pdc.Infrastructure.Entities.Identity;

namespace Pdc.Infrastructure.Entities.Versioning;

public class ComplementaryInformationEntity
{
    public Guid? Id { get; set; }
    public required ChangeableEntity Changeable { get; set; }
    /// <summary>
    /// Version à laquelle l'information a été ajoutée
    /// </summary>
    public required ChangeRecordEntity WrittenOnVersion { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public DateTime CreatedOn { get; set; }
    public required IdentityUserEntity CreatedBy { get; set; }

    public required string Text { get; set; }

    internal void SetCreatedBy(IdentityUserEntity createdBy)
    {
        CreatedBy = createdBy;
    }
}
