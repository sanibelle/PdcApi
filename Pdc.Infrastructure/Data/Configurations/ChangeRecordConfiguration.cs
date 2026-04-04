using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pdc.Infrastructure.Entities.Version;
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

        builder.Property(x => x.ChangeRecordNumber)
            .IsRequired();

        //Not really one to many, but ef needs that
        builder.HasOne(x => x.ParentChangeRecord)
            .WithMany()
            .HasForeignKey(x => x.ChangeRecordId);
        ;

        //Not really one to many, but ef needs that
        builder.HasOne(x => x.NextChangeRecord)
            .WithMany()
            .HasForeignKey(x => x.NextChangeRecordId);

        builder.HasOne(x => x.ValidatedBy)
            .WithMany()
            .HasForeignKey(c => c.ValidatedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.CreatedBy)
            .WithMany()
            .HasForeignKey(c => c.CreatedById)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}
