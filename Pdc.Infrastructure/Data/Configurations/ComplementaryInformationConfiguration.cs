using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pdc.Domain.Entities.Versioning;

namespace Pdc.Infrastructure.Data.Configurations;
public class ComplementaryInformationConfiguration : IEntityTypeConfiguration<ComplementaryInformation>
{
    public void Configure(EntityTypeBuilder<ComplementaryInformation> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Changeable)
            .WithMany()
            .HasForeignKey("ChangeableId")
            .IsRequired();

        builder.HasOne(x => x.WrittenOnVersion)
            .WithMany()
            .HasForeignKey("ChangeRecordId")
            .IsRequired();

        builder.Property(x => x.ModifiedOn)
            .HasDefaultValueSql("GETDATE()")
            .IsRequired();

        builder.Property(x => x.Text)
            .IsRequired()
            .HasMaxLength(5000);
    }
}