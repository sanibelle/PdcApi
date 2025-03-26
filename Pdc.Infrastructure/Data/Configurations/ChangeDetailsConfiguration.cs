using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pdc.Domain.Entities.Versioning;
namespace Pdc.Infrastructure.Data.Configurations;

public class ChangeDetailConfiguration : IEntityTypeConfiguration<ChangeDetail>
{
    public void Configure(EntityTypeBuilder<ChangeDetail> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.OldValue)
            .HasMaxLength(Constants.MaxChangeableLength);

        builder.Property(x => x.ChangeType)
            .IsRequired();

        builder.HasOne(x => x.ChangeRecord)
            .WithMany()
            .IsRequired();

        builder.HasOne(x => x.Changeable)
            .WithOne()
            .IsRequired();
    }
}