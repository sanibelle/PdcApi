using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pdc.Infrastructure.Entities.Versioning;
namespace Pdc.Infrastructure.Data.Configurations;

public class ChangeRecordConfiguration : IEntityTypeConfiguration<ChangeRecordEntity>
{
    public void Configure(EntityTypeBuilder<ChangeRecordEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Description)
            .HasMaxLength(5000);

        builder.Property(x => x.CreatedOn)
            .HasDefaultValueSql("GETDATE()")
            .IsRequired();

        builder.Property(x => x.VersionNumber)
            .UseSequence()
            .IsRequired();

        builder.HasOne(builder => builder.ParentVersion)
            .WithOne()
            .HasForeignKey("VersionId");

        builder.HasOne(builder => builder.NextVersion)
            .WithOne()
            .HasForeignKey("VersionId");
        //UTILISATEUR CreatedBy
    }
}