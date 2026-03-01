using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pdc.Infrastructure.Entities.MinisterialSpecification;
namespace Pdc.Infrastructure.Data.Configurations;

public class CompetencyConfiguration : IEntityTypeConfiguration<CompetencyEntity>
{
    public void Configure(EntityTypeBuilder<CompetencyEntity> builder)
    {
        builder.HasKey(x => x.Code);

        builder.Property(x => x.StatementOfCompetency)
            .IsRequired()
            .HasMaxLength(1500);

        builder.HasMany(x => x.CompetencyElements)
            .WithOne(x => x.Competency)
            .HasForeignKey("CompetencyId");

        builder.HasOne(x => x.Units)
            .WithMany()
            .HasForeignKey(x => x.UnitsId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}