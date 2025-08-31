using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pdc.Domain;
using Pdc.Infrastructure.Entities.Versioning;
namespace Pdc.Infrastructure.Data.Configurations;

public class ChangeableConfiguration : IEntityTypeConfiguration<ChangeableEntity>
{
    public void Configure(EntityTypeBuilder<ChangeableEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Value)
            .HasMaxLength(Constants.MaxChangeableLength);
    }
}