using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pdc.Domain;
using Pdc.Infrastructure.Entities.Versioning;

namespace Pdc.Infrastructure.Data.Configurations;
public class ComplementaryInformationConfiguration : IEntityTypeConfiguration<ComplementaryInformationEntity>
{
    public void Configure(EntityTypeBuilder<ComplementaryInformationEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
        builder.Property(x => x.CreatedOn)
            .HasDefaultValueSql("SYSUTCDATETIME()")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(x => x.Text)
            .IsRequired()
            .HasMaxLength(Constants.MaxComplementaryInformationsLength);

        // TODO mettre a jour le diagramme de la base de données avec les CreatedBy, mettre à jour lesc schemas et les builders.
        builder.HasOne(x => x.CreatedBy)
            .WithMany()
            .HasForeignKey(c => c.CreatedById)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}