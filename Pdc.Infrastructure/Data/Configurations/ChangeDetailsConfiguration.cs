using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pdc.Domain;
using Pdc.Infrastructure.Entities.Versioning;
namespace Pdc.Infrastructure.Data.Configurations;

public class ChangeDetailConfiguration : IEntityTypeConfiguration<ChangeDetailEntity>
{
    public void Configure(EntityTypeBuilder<ChangeDetailEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.OldValue)
            .HasMaxLength(Constants.MaxChangeableLength);

        builder.Property(x => x.ChangeType)
            .IsRequired();

        //builder.HasOne(x => x.ChangeRecord)
        //    .WithMany()
        //    .IsRequired();

        //builder.HasOne(x => x.Changeable)
        //    .WithMany()
        //    .IsRequired();
    }
}