using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pdc.Infrastructure.Entities.Versioning;
namespace Pdc.Infrastructure.Data.Configurations;

public class ChangeRecordConfiguration : IEntityTypeConfiguration<ChangeRecordEntity>
{
    public void Configure(EntityTypeBuilder<ChangeRecordEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Description)
            .HasMaxLength(5000);

        builder.Property(x => x.CreatedOn)
            .HasDefaultValueSql("now()")
            .IsRequired();

        builder.Property(x => x.VersionNumber)
            .IsRequired();

        //Not really one to many, but ef needs that
        builder.HasOne(x => x.ParentVersion)
            .WithMany()
            .HasForeignKey("ParentVersionId");

        //Not really one to many, but ef needs that
        builder.HasOne(x => x.NextVersion)
            .WithMany()
            .HasForeignKey("NextVersionId");

        builder.HasOne(x => x.ValidatedBy)
            .WithMany()
            .HasForeignKey(c => c.ValidatedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.CreatedBy)
            .WithMany()
            .HasForeignKey(c => c.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
